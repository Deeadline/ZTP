using Logg.Logger;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Logg.Output
{
    public class SocketOutput
    {
        private static readonly Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static readonly List<Socket> clientSockets = new List<Socket>();
        private const int buffer_size = 2048;
        private const int port = 20000;
        private static readonly Socket managerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static readonly List<Socket> managerSockets = new List<Socket>();
        private const int port2 = 999;
        private static readonly byte[] buffer2 = new byte[buffer_size];
        private static LogLevel level = LogLevel.Debug;


        public void SetupServer()
        {
            serverSocket.Bind(new IPEndPoint(IPAddress.Any, port));
            serverSocket.Listen(0);
            serverSocket.BeginAccept(AcceptCallback, null);
        }

        public void CloseConnection()
        {
            foreach (var socket in clientSockets)
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            serverSocket.Close();
        }

        private void AcceptCallback(IAsyncResult res)
        {
            Socket socket;
            try
            {
                socket = serverSocket.EndAccept(res);
            }
            catch (ObjectDisposedException)
            {
                return;
            }

            clientSockets.Add(socket);
            socket.BeginReceive(buffer2, 0, buffer_size, SocketFlags.None, ReceiveCallback, socket);
            serverSocket.BeginAccept(AcceptCallback, null);
        }

        public void ReceiveCallback(IAsyncResult res)
        {
            var current = (Socket)res.AsyncState;
            int received;

            try
            {
                received = current.EndReceive(res);
            }
            catch (SocketException)
            {

                current.Close();
                clientSockets.Remove(current);
                return;
            }


            var recBuf = new byte[received];
            Array.Copy(buffer2, recBuf, received);
            var receivedText = Encoding.ASCII.GetString(recBuf);

            try
            {
                if (level == LogLevel.Debug)
                {
                    Write(receivedText, level, current.AddressFamily.ToString(), out var message);
                    var data = Encoding.ASCII.GetBytes(message);
                    current.Send(data);
                }
                else
                {
                    if (receivedText.Length > 10)
                    {
                        Write(receivedText, level, current.AddressFamily.ToString(), out var message);
                        var data = Encoding.ASCII.GetBytes(message);
                        current.Send(data);
                    }
                }
            }
            catch
            {
                var data = Encoding.ASCII.GetBytes("Something went wrong!");
                current.Send(data);
            }
            current.BeginReceive(buffer2, 0, buffer_size, SocketFlags.None, ReceiveCallback, current);

        }

        public void SetupAdminServer()
        {
            managerSocket.Bind(new IPEndPoint(IPAddress.Any, port2));
            managerSocket.Listen(0);
            managerSocket.BeginAccept(AcceptCallback2, null);
        }

        private void AcceptCallback2(IAsyncResult res)
        {
            Socket socket;
            try
            {
                socket = managerSocket.EndAccept(res);
            }
            catch (ObjectDisposedException)
            {
                throw;
            }

            socket.BeginReceive(buffer2, 0, buffer_size, SocketFlags.None, ReceiveCallback2, socket);
            managerSocket.BeginAccept(AcceptCallback2, null);
        }

        private void ReceiveCallback2(IAsyncResult res)
        {
            var current = (Socket)res.AsyncState;
            int received;
            try
            {
                received = current.EndReceive(res);
            }
            catch (SocketException)
            {
                current.Close();
                managerSockets.Remove(current);
                return;
            }

            var recBuffer = new byte[received];
            Array.Copy(buffer2, recBuffer, received);
            var text = Encoding.ASCII.GetString(recBuffer);
            switch (text)
            {
                case "info":
                    level = LogLevel.Info;
                    var data = Encoding.ASCII.GetBytes(DateTime.Now.ToLongTimeString() + " Set Level Log:" + text);
                    current.Send(data);
                    break;
                case "debug":
                    level = LogLevel.Debug;
                    data = Encoding.ASCII.GetBytes(DateTime.Now.ToLongTimeString() + " Set Level Log:" + text);
                    current.Send(data);
                    break;
                case "alert":
                    level = LogLevel.Alert;
                    data = Encoding.ASCII.GetBytes(DateTime.Now.ToLongTimeString() + " Set Level Log:" + text);
                    current.Send(data);
                    break;
                case "error":
                    level = LogLevel.Error;
                    data = Encoding.ASCII.GetBytes(DateTime.Now.ToLongTimeString() + " Set Level Log:" + text + "\n");
                    current.Send(data);
                    break;
                case "exit":
                    current.Shutdown(SocketShutdown.Both);
                    current.Close();
                    managerSockets.Remove(current);
                    return;
                default:
                    data = Encoding.ASCII.GetBytes("Invalid request\n");
                    current.Send(data);
                    break;
            }
            current.BeginReceive(buffer2, 0, buffer_size, SocketFlags.None, ReceiveCallback2, current);
        }

        private static void Write(string message, LogLevel priority, string ipAddress, out string message2)
        {
            message2 = Formatter.Formatter.ServerFormattedMessage(message, priority, ipAddress);
            new FileOutput().Write(message2);
        }
    }
}