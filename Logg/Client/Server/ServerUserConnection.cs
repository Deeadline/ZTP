using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client.Server
{
    public class ServerUserConnection
    {
        private static readonly Socket ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static readonly int port = 20000;

        public static void ConnectToServer()
        {
            ClientSocket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2048));
            int attempts = 0;
            while (!ClientSocket.Connected)
            {
                try
                {
                    Console.WriteLine($"Connection attempt: {attempts++}");
                    ClientSocket.Connect(IPAddress.Loopback, port);
                }
                catch (SocketException)
                {
                    Console.Clear();
                }
            }
            Console.Clear();

            Console.WriteLine("Connected");
            RequestLoop();
        }

        private static void RequestLoop()
        {
            Console.WriteLine("Type exit to disconnect");
            while (true)
            {
                SendRequest();
                ReceiveResponse();
            }
        }

        private static void Exit()
        {
            SendString("exit");
            ClientSocket.Shutdown(SocketShutdown.Both);
            ClientSocket.Close();
            Environment.Exit(0);
        }

        private static void SendRequest()
        {

            Console.Write("Send a request: ");

            string requestM = Console.ReadLine();
            string requestP = Console.ReadLine();

            string request = requestM + " " + requestP;
            SendString(request);


            if (request.ToLower() == "exit")
            {
                Exit();
            }
        }


        private static void SendString(string text)
        {
            var buffer = Encoding.ASCII.GetBytes(text);
            ClientSocket.Send(buffer, 0, buffer.Length, SocketFlags.None);
        }

        private static void ReceiveResponse()
        {
            var buffer = new byte[2048];
            int received = ClientSocket.Receive(buffer, SocketFlags.None);
            if (received == 0) return;
            var data = new byte[received];
            Array.Copy(buffer, data, received);
            string text = Encoding.ASCII.GetString(data);
            if (text == "Invalid request")
            {
                Console.WriteLine(text);
            }
            else if (text == "Bad piority number!")
            {
               Console.WriteLine(text);
            }
            else
            {
                Console.WriteLine(text);

            }
        }
    }
}
