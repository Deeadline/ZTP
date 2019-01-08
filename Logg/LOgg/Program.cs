using Logg.Output;
using System;

namespace Logg
{
    internal static class Program
    {
        //public static readonly Logger.Logger Logger = new Logger.Logger(Output.Output.Socket);

        private static void Main(string[] args)
        {
            //Logger.Log(LogLevel.Info, "TEST 1");
            //Logger.Log(LogLevel.Error, "TEST 1");
            //Logger.Log(LogLevel.Alert, "TEST 1");
            //Logger.Log(LogLevel.Debug, "");


            var server = new SocketOutput();
            server.SetupServer();
            server.SetupAdminServer();
            Console.ReadLine();
            server.CloseConnection();
            Console.ReadKey();
        }
    }
}
