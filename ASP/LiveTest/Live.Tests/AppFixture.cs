using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;


namespace Live.Tests
{
    public class AppFixture
    {
        public const string BaseUrl = "http://localhost:54321";

        static AppFixture()
        {
            IWebHost webHost = WebHost
                                .CreateDefaultBuilder(null)
                                .UseStartup<Startup>()
                                .UseUrls(BaseUrl)
                                .Build();

            webHost.Start();
        }

        public async Task ExecuteHttpClientAsync(Func<HttpClient, Task> action)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(BaseUrl);

            using(httpClient)
            {
                await action(httpClient);
            }
        }

        public string GetCompleteServerUrl(string route)
        {
            route = route.TrimStart('/', '\\');
            return $"{BaseUrl}/{route}";
        }
    }
}

