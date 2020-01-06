using System;

namespace CSharp.Http
{
    public class HttpSample
    {
        public static void Server()
        {
            HttpServer server = new HttpServer();
            server.Run();
        }

        public static void LongPollClient()
        {
            var client = new HttpLongPoll();
            Console.WriteLine("poll request sent");
            client.Run();
        }
    }
}