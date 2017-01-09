using NTwitch.Chat.API;
using NTwitch.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch.Chat
{
    public partial class TwitchChatClient : BaseTwitchClient, ITwitchClient
    {
        public ChatTcpClient TcpClient => _tcp;

        private ChatTcpClient _tcp;
        private MessageParser _parser;
        private string _ircurl;
        private int _port;

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

        public async Task LoginAsync(string username, string token)
        {
            if (_tcp == null)
                throw new InvalidOperationException("You must connect before logging in.");

            await _tcp.LoginAsync(username, token);
            _parser = new MessageParser(_log, _tcp);
            _tcp.MessageReceived += OnMessageReceived;
        }

        public void Disconnect()
        {
            _tcp.Dispose();
            _tcp = null;
        }

        private async Task OnMessageReceived(string content)
        {
            await _parser.Parse(this, content);
        }

        public async Task JoinChannelAsync(string name)
        {
            await _tcp.SendAsync("JOIN #" + name);
        }

        public async Task LeaveChannelAsync(string name)
        {
            await _tcp.SendAsync("PART #" + name);
        }
    }
}
