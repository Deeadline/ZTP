using System.Net;
using System.Net.Sockets;

namespace Admin.Server
{
    public interface ISocketAdmin
    {
        bool Connected();
        void Close();
        void Shutdown(SocketShutdown how);
        int Send(byte[] buffer);
        int Receive(byte[] buffer);
        void Bind(EndPoint localEndPoint);
        void Listen(int backlog);
        ISocketAdmin Accept();
    }
}
