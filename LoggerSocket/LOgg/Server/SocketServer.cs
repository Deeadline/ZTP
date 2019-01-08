using System;
using System.Net;
using System.Net.Sockets;

namespace Logg.Server
{
    public class SocketServer : ISocketServer
    {
        private readonly Socket _tcpSocket;

        public SocketServer() => _tcpSocket = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

        public SocketServer(IAsyncResult socket) => _tcpSocket = socket as Socket;

        public bool Connected() => _tcpSocket.Connected;

        public void Close() => _tcpSocket.Close();

        public void Shutdown(SocketShutdown how) => _tcpSocket.Shutdown(how);

        public int Send(byte[] buffer) => _tcpSocket.Send(buffer);

        public int Receive(byte[] buffer) => _tcpSocket.Receive(buffer);

        public void Bind(EndPoint localEndPoint) => _tcpSocket.Bind(localEndPoint);

        public void Listen(int backlog) => _tcpSocket.Listen(backlog);

        public ISocketServer Accept(AsyncCallback callback) => new SocketServer(_tcpSocket.BeginAccept(callback, null));

        public Socket EndAccept(IAsyncResult res) => _tcpSocket.EndAccept(res);
    }
}
