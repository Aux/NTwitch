using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch.Chat.API
{
    internal class MessageParser
    {
        private ChatTcpClient _client;
        private LogManager _log;

        public MessageParser(LogManager log, ChatTcpClient client)
        {
            _client = client;
            _log = log;
        }

        public async Task Parse(string message)
        {
            if (message == "PING :tmi.twitch.tv")
                await _client.SendAsync("PONG :tmi.twitch.tv");
        }
    }
}
