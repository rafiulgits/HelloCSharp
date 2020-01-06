using System;
using System.Text;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace CSharp.Http
{
    public class HttpServer
    {
        private const string ROOT = "http://localhost:7010/";

        private readonly HttpListener server = new HttpListener();

        public HttpServer()
        {
            server.Prefixes.Add(ROOT);
        }

        public void Run()
        { 
            Console.WriteLine($"server listening on {ROOT}");
            server.Start();

            // handle requests
            Task listenTask = HandleRequests();
            listenTask.GetAwaiter().GetResult();

            server.Close();
        }


        private async Task HandleRequests()
        {
            bool runserver = true;

            while(runserver)
            {
                // listen a client request
                HttpListenerContext context = await server.GetContextAsync();

                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;

                if(request.Url.AbsolutePath == "/favicon.ico")
                {
                    continue;
                }

                PrintRequestLog(request);

                if(request.Url.AbsolutePath == "/shutdown")
                {
                    Console.WriteLine("server close request");
                    runserver = false;
                }

                if(request.Url.AbsolutePath == "/longpoll")
                {
                    try
                    {
                        Thread.Sleep(5000);
                    }
                    catch(Exception)
                    {
                        Console.WriteLine("long poll waiting exception");
                    }
                }
                
                SendResponse(response);
            }
        }

        private async void SendResponse(HttpListenerResponse response)
        {
            byte[] body = Encoding.UTF8.GetBytes("{request : OK}");
            response.ContentType = "application/json";
            response.ContentEncoding = Encoding.UTF8;
            response.ContentLength64 = body.Length;
            await response.OutputStream.WriteAsync(body, 0, body.Length);
            response.Close();
        }

        private void PrintRequestLog(HttpListenerRequest request)
        {
            Console.WriteLine($"[{request.HttpMethod}] : {DateTime.UtcNow.ToLocalTime()} - {request.Url.AbsolutePath}");
        }
    }
}