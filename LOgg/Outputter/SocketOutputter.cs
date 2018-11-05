using System.Diagnostics.Contracts;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ZTPProj3.Outputter
{
    public class SocketOutputter : IOutputter
    {
        private Socket _socketServer;
        private Socket _socketClient;
        private Socket _socketInternal;
        private byte[] buffer = new byte[256];
        public void Write(string message)
        {
            Connect();
            Send(message);
            Disconnect();
            Contract.EnsuresOnThrow<SocketException>(!_socketClient.Connected, "Socket is not closed");
        }

        private void Send(string message)
        {
            Contract.Requires<SocketException>(_socketClient.Connected, "Socket not opened");
            _socketClient.Send(UTF8.GetBytes($"{Dns.GetHostName()}:{message}"));
            _socketInternal.Receive(buffer);
            System.Console.WriteLine(UTF8.GetString(buffer));
        }

        private void Connect()
        {
            _socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var ip = IPAddress.Parse("127.0.0.1");
            var remoteEndPoint = new IPEndPoint(ip, 80);
            _socketServer.Bind(remoteEndPoint);
            _socketServer.Listen(1);
            _socketClient.Connect("127.0.0.1", 80);
            _socketInternal = _socketServer.Accept();

        }

        private void Disconnect()
        {
            _socketClient.Disconnect(false);
            _socketClient.Close();
        }
    }
}