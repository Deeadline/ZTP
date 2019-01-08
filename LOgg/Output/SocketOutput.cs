using System.Diagnostics.Contracts;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Logg.Output
{
    public class SocketOutput : IOutput
    {
        private Socket _socketServer;
        private Socket _socketClient;
        private Socket _socketInternal;
        private readonly byte[] _buffer = new byte[256];
        public void Write(string message)
        {
            Connect();
            Send(message);
            Contract.EnsuresOnThrow<SocketException>(!_socketClient.Connected, "Socket is not closed");
            Disconnect();
        }

        private void Send(string message)
        {
            Contract.Requires<SocketException>(_socketClient.Connected, "Socket not opened");
            _socketClient.Send(Encoding.UTF8.GetBytes($"{Dns.GetHostName()}:{message}"));
            _socketInternal.Receive(_buffer);
            System.Console.WriteLine(Encoding.UTF8.GetString(_buffer));
        }

        private void Connect()
        {
            _socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var ip = IPAddress.Parse("127.0.0.1");
            var remoteEndPoint = new IPEndPoint(ip, 1024);
            _socketServer.Bind(remoteEndPoint);
            _socketServer.Listen(1);
            _socketClient.Connect("127.0.0.1", 1024);
            _socketInternal = _socketServer.Accept();

        }

        private void Disconnect() => _socketServer.Close();
    }
}