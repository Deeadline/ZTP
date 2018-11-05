using System.Collections.Generic;

namespace Logg.Output
{
    public class OutputProvider
    {
        private IOutput _outputter;

        private static readonly Dictionary<Output, IOutput> EnumToOutputter = new Dictionary<Output, IOutput>
        {
            { Output.Console, new ConsoleOutput() },
            { Output.File, new FileOutput() },
            { Output.Socket, new SocketOutput() }
        };

        public void SetOutputter(Output eOutput) => _outputter = EnumToOutputter[eOutput];

        public IOutput GetOutput() => _outputter;
    }
}