using Moq;
using System;
using System.Net;
using System.Net.Sockets;

namespace Logg.Server
{
    public class MockSocketServer : ISocketServer
    {
        private readonly Mock<ISocketServer> _moq;

        public MockSocketServer() => _moq = new Mock<ISocketServer>();

        public bool Connected() => _moq.Object.Connected();

        public void Close() => _moq.Object.Close();

        public void Shutdown(SocketShutdown how) => _moq.Object.Shutdown(how);

        public int Send(byte[] buffer) => _moq.Object.Send(buffer);

        public int Receive(byte[] buffer) => _moq.Object.Receive(buffer);

        public void Bind(EndPoint localEndPoint) => _moq.Object.Bind(localEndPoint);

        public void Listen(int backlog) => _moq.Object.Listen(backlog);

        public ISocketServer Accept(AsyncCallback callback) => _moq.Object.Accept(callback);
        public Socket EndAccept(IAsyncResult res) => _moq.Object.EndAccept(res);

        public void VerifyBind(EndPoint localEp) => _moq.Verify(m => m.Bind(localEp));
        public void VerifyListen(int backlog) => _moq.Verify(m => m.Listen(backlog));
        public void VerifySend(byte[] buffer) => _moq.Verify(m => m.Send(buffer));
        public void VerifyAccept(AsyncCallback callback) => _moq.Verify(m => m.Accept(callback), Times.Once);
        public void VerifyClose() => _moq.Verify(m => m.Close(), Times.Once);
        public void VerifyShutdown(SocketShutdown how) => _moq.Verify(m => m.Shutdown(how), Times.Once);
        public void VerifyReceive(byte[] buffer) => _moq.Verify(m => m.Receive(buffer));

        public void VerifyEndAccept(IAsyncResult res) => _moq.Verify(m => m.EndAccept(res), Times.Once);
    }
}
