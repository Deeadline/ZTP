using Admin.Server;
using Logg.Server;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Logg.Tests
{
    [TestFixture]
    public class LoggTests
    {
        [Test]
        public void ShutdownServer_WhenMessageIsExit_ShouldCloseConnection()
        {
            var adminSocket = new MockSocketAdmin();
            var server = new ServerAdminConnection(adminSocket);
            server.Connect("127.0.0.2", 1024);
            adminSocket.VerifyBind(new IPEndPoint(IPAddress.Parse("127.0.0.2"), 1024));
            server.SendRequest("exit");
            adminSocket.VerifySend(Encoding.UTF8.GetBytes("exit"));
            adminSocket.VerifyClose();
            adminSocket.VerifyShutdown(SocketShutdown.Both);
        }

        [Test]
        public void Test()
        {
            var adminSocket = new MockSocketAdmin();
            var adminServer = new ServerAdminConnection(adminSocket);
            var serverSocket = new MockSocketServer();
            var server = new LoggServer(serverSocket);

            server.SetupAdminServer();
            serverSocket.VerifyBind(new IPEndPoint(IPAddress.Any, 999));
            serverSocket.VerifyListen(0);
            adminServer.Connect("127.0.0.2", 999);
            adminSocket.VerifyBind(new IPEndPoint(IPAddress.Parse("127.0.0.2"), 999));
            adminServer.SendRequest("info");
            adminSocket.VerifySend(Encoding.UTF8.GetBytes("info"));
            serverSocket.VerifyReceive(Encoding.UTF8.GetBytes("info"));
            adminServer.Exit();
            server.CloseClientConnection();
        }
    }
}
