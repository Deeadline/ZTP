using System.Diagnostics.Contracts;
using System.IO;

namespace Logg.Output
{
    public class FileOutput : IOutput
    {
        private const string _filePath = @"C:\Users\MIKI\Desktop\ZTP\Logs.txt";
        private StreamWriter _file;

        public void Write(string message)
        {
            Contract.EnsuresOnThrow<IOException>(_file == null, "file is not closed");
            OpenStream();
            WriteLine(message);
            CloseStream();
        }

        private void WriteLine(string message)
        {
            Contract.Requires<IOException>(_file != null, "_file is not opened");
            _file.WriteLine(message);
        }
        private void OpenStream()
        {
            _file = File.Exists(_filePath) ? File.AppendText(_filePath) : File.CreateText(_filePath);
        }

        private void CloseStream()
        {
            if(_file != null)
                _file.Close();
        }
    }
}