using System;
using System.IO;
using System.Net.Http;
using System.Threading;

namespace CSharp.Http
{
    public class HttpLongPoll
    {
        private readonly HttpClient client = new HttpClient();
        private const string url = "https://localhost:7010/longpoll";

        public void Run()
        {
            using(var request = MakeRequest())
            {
                Response(request);
            }

        }

        private HttpRequestMessage MakeRequest()
        {
            client.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            return request;
        }


        private async void Response(HttpRequestMessage request)
        {
            var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            var body = await response.Content.ReadAsStreamAsync();
            using(var reader = new StreamReader(body))
            {
                while(!reader.EndOfStream)
                {
                    Console.WriteLine(reader.ReadLine());
                }
            }
        }
    }
}