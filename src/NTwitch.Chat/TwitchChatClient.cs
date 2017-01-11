using NTwitch.Rest;
using System.Threading.Tasks;

namespace NTwitch.Chat
{
    public partial class TwitchChatClient : BaseRestClient, ITwitchClient
    {
        public ChatClient Client => _chat;

        private ChatClient _chat;
        private LogManager _log;
        private string _host;
        private int _port;

        public TwitchChatClient() : this(new TwitchChatConfig()) { }
        public TwitchChatClient(TwitchChatConfig config)
        {
            _host = config.ChatUrl;
            _log = new LogManager(config.LogLevel);
        }
        
        public async Task LoginAsync(string username, string token, string clientid)
        {
            await LoginInternalAsync("");
            await LoginAsync(username, token);
        }

        public async Task LoginAsync(string username, string token)
        {
            await _chat.LoginAsync(username, token);
        }

        public async Task ConnectAsync()
        {
            _chat = new ChatClient(_log, _host, _port);
            _chat.MessageReceived += ChatParser.MessageReceived;
            await _chat.ConnectAsync();
        }

        public void DisconnectAsync()
        {
            _chat.Dispose();
        }
    }
}
