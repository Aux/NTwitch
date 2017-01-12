using NTwitch.Rest;
using System.Threading.Tasks;

namespace NTwitch.Chat
{
    public partial class TwitchChatClient : BaseRestClient, ITwitchClient
    {
        public ChatClient Client => _chat;

        private ChatClient _chat;
        private string _host;
        private int _port;

        public TwitchChatClient() : this(new TwitchChatConfig()) { }
        public TwitchChatClient(TwitchChatConfig config) : base(config)
        {
            _host = config.ChatUrl;
        }
        
        public async Task LoginAsync(string username, string token, string clientid)
        {
            await LoginInternalAsync(clientid, token);
            await LoginAsync(username, token);
        }

        public async Task LoginAsync(string username, string token)
        {
            await _chat.LoginAsync(username, token);
        }

        public async Task ConnectAsync()
        {
            _chat = new ChatClient(Logger, _host, _port);
            _chat.MessageReceived += ChatParser.MessageReceived;
            await _chat.ConnectAsync();
        }

        public void DisconnectAsync()
        {
            _chat.Dispose();
        }
    }
}
