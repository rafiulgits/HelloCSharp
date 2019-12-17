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
            // services.Configure<DatabaseSettings>(options => 
            //     configuration.GetSection("MongoDBSettings").Bind(options));

            // // make database connection service singleton
            // services.AddSingleton<DatabaseSettings>(options => 
            //     options.GetRequiredService<IOptions<DatabaseSettings>>().Value);
            
            DatabaseSettings databaseSettings = new DatabaseSettings();
            configuration.Bind("MongoDBSettings", databaseSettings);
            services.AddSingleton(databaseSettings);

            // model repository as singleton service
            services.AddSingleton<UserService>();
            services.AddSingleton<PostService>();
        }
    }
}