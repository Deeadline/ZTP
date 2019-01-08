using Logg.Output;
using System;

namespace Logg
{
    internal static class Program
    {
        private static void Main(string[] args)
        {

            var server = new SocketOutput();
            server.SetupServer();
            server.SetupAdminServer();
            Console.ReadLine();
            server.CloseConnection();
            Console.ReadKey();
        }
    }
}
