using System;
using System.Threading.Tasks;

namespace NTwitch.WebSocket
{
    public class TwitchSocketClient : BaseSocketClient, ITwitchClient
    {
        public TwitchSocketClient() : this(new TwitchSocketConfig()) { }
        public TwitchSocketClient(TwitchSocketConfig config) : base(config) { }
        
        public Task LoginAsync(TokenType type, string token)
            => SocketLoginAsync(type, token);
        
        Task ITwitchClient.ConnectAsync()
            => throw new NotSupportedException();
        Task ITwitchClient.DisconnectAsync()
            => throw new NotSupportedException();
    }
}
