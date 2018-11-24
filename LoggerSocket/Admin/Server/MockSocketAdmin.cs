using Moq;
using System.Net;
using System.Net.Sockets;

namespace Admin.Server
{
    public class MockSocketAdmin : ISocketAdmin
    {
        private readonly Mock<ISocketAdmin> _moq;

        public MockSocketAdmin() => _moq = new Mock<ISocketAdmin>();

        public bool Connected() => _moq.Object.Connected();

        public void Close() => _moq.Object.Close();

        public void Shutdown(SocketShutdown how) => _moq.Object.Shutdown(how);

        public int Send(byte[] buffer) => _moq.Object.Send(buffer);

        public int Receive(byte[] buffer) => _moq.Object.Receive(buffer);

        public void Bind(EndPoint localEndPoint) => _moq.Object.Bind(localEndPoint);

        public void Listen(int backlog) => _moq.Object.Listen(backlog);

        public ISocketAdmin Accept() => _moq.Object.Accept();
        public void VerifyBind(EndPoint localEp) => _moq.Verify(m => m.Bind(localEp), Times.Once);
        public void VerifyListen(int backlog) => _moq.Verify(m => m.Listen(backlog), Times.AtLeastOnce);
        public void VerifySend(byte[] buffer) => _moq.Verify(m => m.Send(buffer), Times.AtLeastOnce);
        public void VerifyAccept() => _moq.Verify(m => m.Accept(), Times.Once);
        public void VerifyClose() => _moq.Verify(m => m.Close(), Times.Once);
        public void VerifyShutdown(SocketShutdown how) => _moq.Verify(m => m.Shutdown(how), Times.Once);
        public void VerifyReceive(byte[] buffer) => _moq.Verify(m => m.Receive(buffer), Times.Once);
    }
}
