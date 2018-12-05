using System.Net;
using System.Net.Sockets;

namespace Admin.Server
{
    public class SocketAdmin : ISocketAdmin
    {
        private readonly Socket _tcpSocket;

        public SocketAdmin() => _tcpSocket = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

        public SocketAdmin(Socket socket) => _tcpSocket = socket;

        public bool Connected() => _tcpSocket.Connected;

        public void Close() => _tcpSocket.Close();

        public void Shutdown(SocketShutdown how) => _tcpSocket.Shutdown(how);

        public int Send(byte[] buffer) => _tcpSocket.Send(buffer);

        public int Receive(byte[] buffer) => _tcpSocket.Receive(buffer);

        public void Bind(EndPoint localEndPoint) => _tcpSocket.Bind(localEndPoint);

        public void Listen(int backlog) => _tcpSocket.Listen(backlog);

        public ISocketAdmin Accept() => new SocketAdmin(_tcpSocket.Accept());
    }
}
