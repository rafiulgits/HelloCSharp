using API.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Config
{
    public class Auth
    {
        private static string SECRET_KEY = "qwertyuioplkjhgfdsazxcvbnmqwertlkjfdslkjflksjfklsjfklsjdflskjflyuiop";
        
        public static string GetToken(User user)
        {
            byte[] secretKey = Encoding.UTF8.GetBytes(SECRET_KEY);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(secretKey);
            SigningCredentials signingCredentials = new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256Signature
            );
            ClaimsIdentity parameters = new ClaimsIdentity();
            parameters.AddClaim(new Claim(ClaimTypes.Name  , user.UserName));
            parameters.AddClaim(new Claim(ClaimTypes.Email , user.Email));
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor {
                Subject = parameters,
                Expires = DateTime.UtcNow.AddMinutes(120),
                SigningCredentials = signingCredentials
            };
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }

        private static ClaimsPrincipal GetPrincipal(string token)
        {
             try
            {
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = (JwtSecurityToken)handler.ReadJwtToken(token);
                if(jwtToken == null)
                {
                    return null;
                }
                byte[] secretKey = Encoding.UTF8.GetBytes(SECRET_KEY);
                SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(secretKey);
                TokenValidationParameters parameters = new TokenValidationParameters {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = symmetricSecurityKey
                };
                SecurityToken securityToken;
                ClaimsPrincipal principal = handler.ValidateToken(token, parameters, out securityToken);
                return principal;            
            }
            catch(Exception)
            {
                return null;
            }
        }


        public static string ValidateToken(string token)
        {
           ClaimsPrincipal principal = GetPrincipal(token);
           if(principal == null)
           {
               return null;
           }
           ClaimsIdentity identity = null;
           try 
           {
               identity = (ClaimsIdentity)principal.Identity;
           }
           catch(NullReferenceException)
           {
               return null;
           }
           return identity.FindFirst(ClaimTypes.Name).Value;
        }
    }
}