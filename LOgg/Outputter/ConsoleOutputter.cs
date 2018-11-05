using System;

namespace ZTPProj3.Outputter
{
    public class ConsoleOutputter : IOutputter
    {
        public void Write(string message) => Console.WriteLine(message);
    }
}