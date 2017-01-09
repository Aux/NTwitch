using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace NTwitch.Chat.API
{
    public class ChatTcpClient : IDisposable
    {
        private TcpClient _tcp;
        private string _url;
        private int _port;

        public ChatTcpClient(LogManager log, string url, int port)
        {
            _url = url;
            _port = port;
        }
        
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
