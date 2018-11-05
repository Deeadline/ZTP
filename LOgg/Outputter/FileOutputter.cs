using System.Diagnostics.Contracts;
using System.IO;

namespace ZTPProj3.Outputter
{
    public class FileOutputter : IOutputter
    {
        private const string _filePath = @"C:\Users\MIKI\Desktop\ZTP\Logs.txt";
        private StreamWriter _file;

        public void Write(string message)
        {
            OpenStream();
            Contract.Requires<IOException>(_file != null, "_file is not opened");
            _file?.WriteLine(message);
            CloseStream();
            Contract.EnsuresOnThrow<IOException>(_file == null, "file is not closed");
        }

        private void OpenStream() => _file = File.Exists(_filePath) ? File.AppendText(_filePath) : File.CreateText(_filePath);

        private void CloseStream() => _file?.Close();
    }
}