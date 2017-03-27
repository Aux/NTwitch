using System;
using System.Threading.Tasks;

namespace NTwitch.Chat
{
    public class ChatApiClient : IDisposable
    {
        private ChatClient _client;
        private TwitchChatConfig _config;
        private bool _disposed = false;

        public ChatApiClient(TwitchChatConfig config, string username, string token)
        {
            _config = config;
            _client = new ChatClient(config, username, token);
        }

        public Task ConnectAsync()
            => _client.ConnectAsync();
        
        public Task DisconnectAsync()
            => _client.DisconnectAsync();
        
        public Task SendAsync(ChatRequest request)
            => _client.SendAsync(request);
        
        
        #region Actions

        #endregion
        
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _client.Dispose();
                }
                
                _disposed = true;
            }
        }
        
        public void Dispose()
        {
            Dispose(true);
        }
    }
}
