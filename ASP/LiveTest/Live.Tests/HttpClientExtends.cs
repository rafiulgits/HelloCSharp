using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Live.Models;

namespace Live.Tests
{
    public static class HttpClientExtends
    {
        public static async Task<HttpResponseMessage> PostAsJsonAsync(this HttpClient httpClient,string url, Notification notification)
        {
            var dataAsString = JsonConvert.SerializeObject(notification);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return await httpClient.PostAsync(url, content);
        }
    }
}