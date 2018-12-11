using System;
using System.Net;
using System.Net.Sockets;

namespace Logg.Server
{
    public interface ISocketServer
    {
        bool Connected();
        void Close();
        void Shutdown(SocketShutdown how);
        int Send(byte[] buffer);
        int Receive(byte[] buffer);
        void Bind(EndPoint localEndPoint);
        void Listen(int backlog);
        ISocketServer Accept(AsyncCallback callback);
        Socket EndAccept(IAsyncResult res);
    }
}
