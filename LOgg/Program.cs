using ZTPProj3.Logger;
using ZTPProj3.Outputter;

namespace ZTPProj3
{
    internal static class Program
    {
        public static readonly Logger.Logger Logger = new Logger.Logger(Output.Socket);

        private static void Main(string[] args)
        {
            Logger.Log(LogLevel.Debug,"TEST 1");
            Logger.Log(LogLevel.Info,"TEST 1");
            Logger.Log(LogLevel.Error, "TEST 1");
            Logger.Log(null);
        }
    }
}
