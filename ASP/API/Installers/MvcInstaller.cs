using API.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace API.Installers
{
    public class MvcInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            InstallJwtAuthentication(services, configuration);
            
            services.AddSwaggerGen(op =>{
                op.SwaggerDoc("v1", new OpenApiInfo{Title="API", Version="v1"});

                op.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme{
                    Description = "JWT Authorization header using the Bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                op.AddSecurityRequirement(new OpenApiSecurityRequirement{
                   {
                       new OpenApiSecurityScheme {
                           Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme, Id="Bearer"}
                       },
                       new string[] {}
                   }
                });
            });
        }

        private void InstallJwtAuthentication(IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = new JwtSettings();
            configuration.Bind(nameof(JwtSettings), jwtSettings);
            services.AddSingleton(jwtSettings);
            
            services.AddAuthentication(op => {
                op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(op=> {
                op.SaveToken = true;
                op.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = false,
                    ValidateLifetime = true
                };
            });

            // map JwtSettings to AppSettingsProvider
            AppSettingsProvider.jwtSettings = jwtSettings;
        }
    }
}