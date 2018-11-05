using System.Collections.Generic;

namespace ZTPProj3.Outputter
{
    public class OutputProvider
    {
        private IOutputter _outputter;

        private static readonly Dictionary<Output, IOutputter> EnumToOutputter = new Dictionary<Output, IOutputter>
        {
            { Output.Console, new ConsoleOutputter() },
            { Output.File, new FileOutputter() },
            { Output.Socket, new SocketOutputter() }
        };

        public void SetOutputter(Output eOutput) => _outputter = EnumToOutputter[eOutput];

        public IOutputter GetOutput() => _outputter;
    }
}