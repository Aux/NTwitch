using System;
using System.Threading.Tasks;

namespace NTwitch.WebSocket
{
    public class PubsubApiClient : IDisposable
    {
        private SocketClient _client;

        private bool _disposed = false;

        public PubsubApiClient(TwitchSocketConfig config, TokenType type, string token)
        {
            _client = new SocketClient(config, type, token);
        }

        public Task SendAsync(string method, string topic)
        {
            throw new NotImplementedException();
        }

        #region Channels

        internal Task JoinChannelAsync(string name)
        {
            throw new NotImplementedException();
        }
        internal Task LeaveChannelAsync(string name)
        {
            throw new NotImplementedException();
        }

        #endregion


        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
