using NTwitch.Rest;
using System;
using System.Threading.Tasks;

namespace NTwitch.WebSocket
{
    public partial class BaseSocketClient : BaseRestClient
    {
        public ChatApiClient ChatClient => _chat;
        public PubsubApiClient PubsubClient => _pubsub;

        private ChatApiClient _chat;
        private PubsubApiClient _pubsub;
        private ISocketCache _cache;
        private TwitchSocketConfig _config;
        
        public BaseSocketClient(TwitchSocketConfig config) : base(config)
        {
            _cache = _config.CacheInstance;
            _config = config;
        }

        internal async Task SocketLoginAsync(TokenType type, string token)
        {
            await RestLoginAsync(type, token);
            _chat = new ChatApiClient(_config, type, token);
        }

        internal Task ConnectInternalAsync()
        {
            throw new NotImplementedException();
        }

        internal Task DisconnectInternalAsync()
        {
            throw new NotImplementedException();
        }

        // Channel
        public Task JoinChannelAsync(string name)
            => _chat.JoinChannelAsync(name);
        public Task LeaveChannelAsync(string name)
            => _chat.LeaveChannelAsync(name);
    }
}
