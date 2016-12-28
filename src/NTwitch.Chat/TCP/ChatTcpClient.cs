using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace NTwitch.Chat
{
    public class ChatTcpClient : IDisposable
    {
        private TcpClient _tcp;
        private LogManager _log;
        private string _host;
        private int _port;
        
        public ChatTcpClient(LogManager log, string host, int port)
        {
            _log = log;

        }



        public async Task LoginAsync()
        {
            await Task.Delay(1);
        }

        public async Task ConnectAsync()
        {
            await Task.Delay(1);
        }

        public async Task DisconnectAsync()
        {
            await Task.Delay(1);
        }

        public void Dispose()
        {
            _tcp.Dispose();
        }
    }
}
