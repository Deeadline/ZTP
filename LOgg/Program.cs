using Logg.Logger;
using System;

namespace Logg
{
    internal static class Program
    {
        public static readonly Logger.Logger Logger = new Logger.Logger(Output.Output.Socket);

        private static void Main(string[] args)
        {
            Logger.Log(LogLevel.Info, "TEST 1");
            Logger.Log(LogLevel.Error, "TEST 1");
            Logger.Log(LogLevel.Alert, "TEST 1");
            Logger.Log(LogLevel.Debug, null);

            Console.ReadKey();
        }
    }
}
