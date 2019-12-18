using API.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;


class JwtToken
{
    private static JwtSecurityTokenHandler tokenHandler;

    public static JwtSecurityTokenHandler GetHandler()
    {
        if(tokenHandler == null)
        {
            tokenHandler = new JwtSecurityTokenHandler();
        }
        return tokenHandler;
    }
}


namespace API.Models.Extension
{
    public static class UserExtension
    {
        public static IEnumerable<User> WithoutPasswords(this IEnumerable<User> users)
        {
            return users.Select(user => user.WithoutPassword());
        }

        public static User WithoutPassword(this User user)
        {
            user.Password = null;
            return user;
        }

        public static string GetToken(this User user)
        {
            var tokenHandler = JwtToken.GetHandler();
            var key = Encoding.ASCII.GetBytes(AppSettingsProvider.jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private static string GetEncryptedValue(string password)
        {
            string hashValue = System.Convert.ToBase64String(Microsoft.AspNetCore.Cryptography.KeyDerivation.KeyDerivation.Pbkdf2(
                password : password,
                salt : System.Text.Encoding.UTF8.GetBytes(AppSettingsProvider.jwtSettings.Secret),
                prf : Microsoft.AspNetCore.Cryptography.KeyDerivation.KeyDerivationPrf.HMACSHA256,
                iterationCount : 1000,
                numBytesRequested: 256/8
            ));
            return hashValue;
        }

        public static void SetPassword(this User user, string rawPassword)
        {
            user.Password = GetEncryptedValue(rawPassword);
        }

        public static bool CheckPassword(this User user, string rawPassword)
        {
            return user.Password == GetEncryptedValue(rawPassword);
        }
    }    
}