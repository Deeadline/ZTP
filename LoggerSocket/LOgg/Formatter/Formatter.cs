using Logg.Logger;
using System;

namespace Logg.Formatter
{
    public static class Formatter
    {
        public static string StandardFormattedMessage(string message, LogLevel priority)
        {
            return $"[{DateTime.Now.ToLongDateString()}]:${priority}:${message}\n";
        }

        public static string ServerFormattedMessage(string message, LogLevel priority, string ipAddress)
        {
            return $"${priority}:${ipAddress}:[{DateTime.Now.ToLongDateString()}]:${message}\n";
        }
    }
}
