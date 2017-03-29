using NTwitch.Rest;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch.Pubsub
{
    public partial class BasePubsubClient : BaseRestClient
    {
        public IReadOnlyDictionary<string, Func<string, Task>> Subcriptions => _pubsub.Callbacks;
        public PubsubApiClient PubsubClient => _pubsub;
        
        private PubsubApiClient _pubsub;
        private TwitchPubsubConfig _config;
        
        public BasePubsubClient(TwitchPubsubConfig config) : base(config)
        {
            _config = config;
        }

        internal async Task SocketLoginAsync(AuthMode type, string token)
        {
            await RestLoginAsync(type, token);
            _pubsub = new PubsubApiClient(_config, Logger, type, token);
        }

        internal async Task ConnectInternalAsync()
        {
            await _pubsub.ConnectAsync();
            await connectedEvent.InvokeAsync().ConfigureAwait(false);
        }

        internal async Task DisconnectInternalAsync()
        {
            await _pubsub.DisconnectAsync();
            await disconnectedEvent.InvokeAsync().ConfigureAwait(false);
        }

        public Task SubscribeAsync(string topic, string parameters, Func<string, Task> callback)
            => _pubsub.SendAsync(topic, parameters);
        public Task UnsubscribeAsync(string topic, string parameters)
            => _pubsub.SendAsync(topic, parameters);
    }
}
