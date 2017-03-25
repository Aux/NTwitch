using System;
using System.Threading.Tasks;

namespace NTwitch.Tcp
{
    public class TcpApiClient : IDisposable
    {
        private TcpClient _client;
        private TwitchTcpConfig _config;
        private bool _disposed = false;

        public TcpApiClient(TwitchTcpConfig config, string username, string token)
        {
            _config = config;
            _client = new TcpClient(config, username, token);
        }

        public Task ConnectAsync()
            => _client.ConnectAsync();
        
        public Task DisconnectAsync()
            => _client.DisconnectAsync();
        
        public Task SendAsync(TcpRequest request)
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
