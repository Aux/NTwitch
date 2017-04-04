using System;
using System.Threading.Tasks;

namespace NTwitch.Chat
{
    public class ChatApiClient
    {
        private SocketClient _client;

        internal LogManager Logger;

        public ChatApiClient(TwitchChatConfig config)
        {
            Logger = new LogManager(config.LogLevel);

            if (config.ChatProvider == null)
                _client = new TcpSocketClient(Logger, config.ChatHost, config.ChatPort);
            else
                _client = config.ChatProvider;

            _client.MessageReceived += OnMessageInternalAsync;
        }

        private Task OnMessageInternalAsync(string msg)
        {
            return Console.Out.WriteLineAsync(msg);
        }

        public Task ConnectAsync()
            => _client.ConnectAsync();
        public Task DisconnectAsync()
            => _client.DisconnectAsync();

        public Task SendAsync(string command, string parameters)
            => SendAsync(new ChatRequest(command, parameters));
        public Task SendAsync(ChatRequest request)
            => _client.SendAsync(request.ToString());

        #region Authorization

        internal async Task AuthorizeAsync(string username, string token)
        {
            await SendAsync("PASS", $"oauth:{token}");
            await SendAsync("NICK", username);
        }

        internal Task RequestTagsAsync()
            => SendAsync("CAP REQ", ":twitch.tv/tags");
        
        internal Task RequestMembershipAsync()
            => SendAsync("CAP REQ", ":twitch.tv/membership");
        
        internal Task RequestCommandsAsync()
            => SendAsync("CAP REQ", ":twitch.tv/commands");

        #endregion
        #region Channels

        internal Task JoinChannelAsync(string name)
            => SendAsync("JOIN", $"#{name}");

        internal Task PartChannelAsync(string name)
            => SendAsync("PART", $"#{name}");

        #endregion
    }
}
