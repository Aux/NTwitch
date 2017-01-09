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

        public async Task Parse(TwitchChatClient client, string message)
        {
            if (message == "PING :tmi.twitch.tv")
            {
                await _client.SendAsync("PONG :tmi.twitch.tv");
                return;
            }

            var parts = message.Split(';');
            var data = new Dictionary<string, string>();
            foreach (var part in parts)
            {
                var pair = part.Split('=');
                data.Add(pair[0], pair[1]);
            }

            await client._messageReceived.InvokeAsync(ChatMessage.Create(data));
        }
    }
}
