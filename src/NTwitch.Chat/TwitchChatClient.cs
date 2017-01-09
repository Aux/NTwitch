using NTwitch.Chat.API;
using NTwitch.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch.Chat
{
    public partial class TwitchChatClient : TwitchRestClient, ITwitchClient
    {
        private ChatTcpClient _tcp;
        private string _ircurl;
        private int _port;

        public ChatTcpClient TcpClient => _tcp;

        public TwitchChatClient() : this(new TwitchChatConfig()) { }
        public TwitchChatClient(TwitchChatConfig config) : base(config)
        {
            _ircurl = config.IrcUrl;
            _port = config.Port;
        }

        public async Task ConnectAsync()
        {
            _tcp = new ChatTcpClient(_log, _ircurl, _port);
            await _tcp.ConnectAsync();
        }

        public Task DisconnectAsync()
        {
            throw new NotImplementedException();
        }
    }
}
