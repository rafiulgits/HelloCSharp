using API.Models;
using API.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace API.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            // map database settings from appsettings to model service
            services.Configure<DatabaseSettings>(options => {
                options.DatabaseHost = configuration.GetSection("MongoDBSettings:DBHost").Value;
                options.DatabaseName = configuration.GetSection("MongoDBSettings:DBName").Value;
            });

            // make database connection service singleton
            services.AddSingleton<DatabaseSettings>(options => 
                options.GetRequiredService<IOptions<DatabaseSettings>>().Value);

            // model repository as singleton service
            services.AddSingleton<UserService>();
            services.AddSingleton<PostService>();
        }
    }
}