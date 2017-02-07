using NTwitch.Rest;
using System.Threading.Tasks;

namespace NTwitch.Chat
{
    public partial class TwitchChatClient : BaseRestClient, ITwitchClient
    {
        public ChatClient Client => _chat;

        private ChatClient _chat;
        private ChatHandler _parser;
        private string _host;
        private int _port;
        private bool _modOnly;

        public TwitchChatClient() : this(new TwitchChatConfig()) { }
        public TwitchChatClient(TwitchChatConfig config) : base(config)
        {
            _host = config.ChatUrl;
            _port = config.ChatPort;
            _modOnly = config.CanSpeakWithoutModerator;
        }
        
        public async Task LoginAsync(string username, string token)
        {
            await LoginInternalAsync(TokenType.OAuth, token);
            await _chat.LoginAsync(username, token);
        }

        public async Task ConnectAsync()
        {
            _chat = new ChatClient(Logger, _host, _port, _modOnly);
            _parser = new ChatHandler(this);
            await _chat.ConnectAsync();
        }

        public void DisconnectAsync()
        {
            _chat.Dispose();
        }

        public Task JoinAsync(string channelName)
            => _chat.SendAsync("JOIN #" + channelName);

        public Task LeaveAsync(string channelName)
            => _chat.SendAsync("PART #" + channelName);
    }
}
