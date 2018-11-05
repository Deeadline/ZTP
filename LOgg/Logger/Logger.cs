using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using ZTPProj3.Outputter;

namespace ZTPProj3.Logger
{
    public class Logger
    {
        private readonly OutputProvider _outputService = new OutputProvider();
        private readonly Dictionary<LogLevel, string> _levelText = new Dictionary<LogLevel, string>
        {
            { LogLevel.Info, $"INFO: {DateTime.Now:G} :" },
            { LogLevel.Debug, $"DEBUG: {DateTime.Now:G} :" },
            { LogLevel.Alert, $"ALERT: {DateTime.Now:G} :" },
            { LogLevel.Error, $"ERROR: {DateTime.Now:G} :" },
        };

        public Logger(Output output)
        {
            _outputService.SetOutputter(output);
        }

        public void Log(LogLevel level, string message)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(message), "Message is empty");
            _outputService.GetOutput().Write($"{_levelText[level]} {message}");
        }
    }
}
