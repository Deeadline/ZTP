using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Admin.Server
{
    public class ServerAdminConnection
    {
        private readonly ISocketAdmin _socket;

        public ServerAdminConnection(ISocketAdmin socket) => _socket = socket;

        public void Connect(string ip, int port) => _socket.Bind(new IPEndPoint(IPAddress.Parse(ip), port));

        public void SendRequest(string request)
        {
            _socket.Send(Encoding.UTF8.GetBytes(request));
            if (request.Equals("exit"))
                Exit();
        }

        public void Exit()
        {
            _socket.Shutdown(SocketShutdown.Both);
            _socket.Close();
        }

        public void ReceiveResponse()
        {
            var buffer = new byte[2048];
            var received = _socket.Receive(buffer);
            var data = new byte[received];
            Array.Copy(buffer, data, received);
            Console.WriteLine(Encoding.UTF8.GetString(data));
        }

    }
}
