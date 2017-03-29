using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace NTwitch.Pubsub
{
    public class PubsubApiClient : IDisposable
    {
        private SocketClient _client;

        internal ConcurrentDictionary<string, Func<string, Task>> Callbacks;
        internal LogManager Logger;

        private bool _disposed = false;
        
        public PubsubApiClient(TwitchPubsubConfig config, LogManager logger, AuthMode type, string token)
        {
            Logger = logger;
            _client = new SocketClient(config);
        }

        public Task SendAsync(string method, string topic)
        {
            throw new NotImplementedException();
        }
        
        internal Task ConnectAsync()
        {
            return Task.CompletedTask;
        }

        internal Task DisconnectAsync()
        {
            return Task.CompletedTask;
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    DisconnectAsync().GetAwaiter().GetResult();
                    _client.Dispose();
                }

                _client = null;
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
