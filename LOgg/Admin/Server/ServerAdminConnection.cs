using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Admin.Server
{ 
    public class ServerAdminConnection
    {
        private static readonly Socket AdminSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static int port = 999;
        public static void Connect()
        {
            AdminSocket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.2"), 1024));
            int attempts = 0;
            while (!AdminSocket.Connected)
            {
                try
                {
                    Console.WriteLine($"Connection attempt {attempts++}");
                    AdminSocket.Connect(IPAddress.Loopback, port);
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

            while(true)
            {
                SendRequest();
                ReceiveResponse();
            }
        }
        private static void Exit()
        {
            SendString("exit");
            AdminSocket.Shutdown(SocketShutdown.Both);
            AdminSocket.Close();
            Environment.Exit(0);
        }

        private static void SendRequest()
        {

            Console.Write("Send a request: ");
            string request = Console.ReadLine();
            SendString(request);

            if (request.ToLower() == "exit")
            {
                Exit();
            }
        }



        private static void SendString(string text)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(text);
            AdminSocket.Send(buffer, 0, buffer.Length, SocketFlags.None);
        }

        private static void ReceiveResponse()
        {
            var buffer = new byte[2048];
            int received = AdminSocket.Receive(buffer, SocketFlags.None);
            if (received == 0) return;
            var data = new byte[received];
            Array.Copy(buffer, data, received);
            string text = Encoding.ASCII.GetString(data);
            Console.WriteLine(text);
        }
    }
}
