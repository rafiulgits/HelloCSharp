using System;
using System.Net;
using System.Net.Sockets;
using System.Text;


namespace CSharp.Socket 
{
    namespace TCP
    {
        public class TCPServerSocket {
            private int port = 6235;
            private TcpListener server;
            private TcpClient client;
            private bool isRunning = false;

            public TCPServerSocket(){
                server = new TcpListener(IPAddress.Loopback, port);
                server.Start();
                Run();
            }

            private void Run(){
                int maxConnection = 10;
                int counter = 0;
                isRunning = true;
                while(isRunning && counter < maxConnection){
                    Console.WriteLine("Waiting...");
                    client = server.AcceptTcpClient();
                    isRunning = true;
                    NetworkStream networkStream = client.GetStream();
                    Console.WriteLine("Connection OK!");
                    try{
                        networkStream.Write(Encoding.ASCII.GetBytes("Hello Client"));
                        networkStream.Close();
                        client.Close();
                    } catch(Exception e){
                        Console.WriteLine(e);
                    }
                    counter ++;
                }
                server.Stop();
            }
        }


        public class TCPClientSocket {
            private int port = 6235;
            private TcpClient client;

            public TCPClientSocket(){
                Run();
            }

            private void Run(){
                Console.WriteLine("Connecting...");
                try{
                    client = new TcpClient(IPAddress.Loopback.ToString(), port);
                    Console.WriteLine("Connected");
                    NetworkStream networkStream = client.GetStream();
                    byte[] byteBuffer = new byte[1024*32];
                    int readingBytes = networkStream.Read(byteBuffer, 0, byteBuffer.Length);
                    Console.Write($"[{readingBytes}] : ");
                    Console.WriteLine(Encoding.ASCII.GetString(byteBuffer, 0, readingBytes));
                    networkStream.Close();
                    client.Close();
                } catch(Exception e){
                    Console.WriteLine(e);
                }
            }
        }

        public class TCPSocketExamples{
            public static void TCPServer(){
                new TCPServerSocket();
            }

            public static void TCPClient(){
                new TCPClientSocket();
            }
        }
    }

    namespace UDP
    {
        public class UDPListenerSocket {
            private int port = 6978;
            UdpClient listener;
            IPEndPoint groupEndPoint;

            public UDPListenerSocket(){
                listener = new UdpClient(port);
                groupEndPoint = new IPEndPoint(IPAddress.Any, port);
                Run();
            }

            private void Run(){
                try{
                    Console.WriteLine("Broadcast listening");
                    while(true){
                        byte[] byteBuffer = listener.Receive(ref groupEndPoint);
                        Console.Write("Received broadcast: ");
                        Console.WriteLine(Encoding.ASCII.GetString(byteBuffer, 0, byteBuffer.Length));
                    }
                } catch(Exception e){
                    Console.WriteLine(e);
                } finally {
                    listener.Close();
                }
            }
        }

        public class UDPSenderSocket {
            private int port = 6978;
            private System.Net.Sockets.Socket senderSocket;
            private IPEndPoint endPoint;

            public UDPSenderSocket(){
                senderSocket = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Dgram, 
                    ProtocolType.Udp);
                var broadcastAddress = IPAddress.Parse("192.168.1.255");
                endPoint =  new IPEndPoint(broadcastAddress, port);
                Run();
            }

            private void Run(){
                byte[] message = Encoding.ASCII.GetBytes(Guid.NewGuid().ToString());
                for(int i=0; i<5; i++){
                    senderSocket.SendTo(message, endPoint);
                    Console.WriteLine("Message Sent");
                }
            }
        }
        
        public class UDPSocketExamples {
            public static void UDPListener(){
                new UDPListenerSocket();
            }

            public static void UDPSender(){
                new UDPSenderSocket();
            }
        }
    }
}