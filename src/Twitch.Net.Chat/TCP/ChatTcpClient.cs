using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Twitch.Chat
{
    public class ChatTcpClient : IDisposable
    {
        private TcpClient _tcp;
        
        public ChatTcpClient()
        {

        }

        public async Task LoginAsync()
        {
            await Task.Delay(1);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
