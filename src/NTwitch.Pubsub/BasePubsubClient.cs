using NTwitch.Rest;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch.Pubsub
{
    public partial class BasePubsubClient : BaseRestClient
    {
        public PubsubApiClient PubsubClient => _pubsub;
        
        private PubsubApiClient _pubsub;
        private TwitchPubsubConfig _config;
        private long Latency = -1;
        
        public BasePubsubClient(TwitchPubsubConfig config) : base(config)
        {
            _config = config;
            _pubsub.LatencyUpdated += OnLatencyInternalAsync;
            _pubsub.MessageReceived += OnMessageInternalAsync;
        }

        private Task OnLatencyInternalAsync(long ms)
        {
            Latency = ms;
            return Task.CompletedTask;
        }

        private async Task OnMessageInternalAsync(string topic, string content)
        {
            string title = topic.Split(new[] { '.' }, 2).FirstOrDefault();
            
            switch (title.ToLower())
            {
                case "whispers":
                    await PubsubHelper.HandleWhisperAsync(this, content).ConfigureAwait(false);
                    break;
                case "channel-bits-events-v1":
                    break;
            }
            // Forward to appropriate event
        }

        internal async Task SocketLoginAsync(string token)
        {
            await RestLoginAsync(token);
            _pubsub = new PubsubApiClient(_config, Logger, token);
        }

        internal async Task ConnectInternalAsync()
        {
            //await _pubsub.ConnectAsync();
            await connectedEvent.InvokeAsync().ConfigureAwait(false);
        }

        internal async Task DisconnectInternalAsync()
        {
            //await _pubsub.DisconnectAsync();
            await disconnectedEvent.InvokeAsync().ConfigureAwait(false);
        }

        // Channels
        public Task SubscribePlaybackAsync(IEnumerable<ulong> ids)
            => SubscribePlaybackAsync(ids.ToArray());
        public Task SubscribePlaybackAsync(params ulong[] ids)
            => _pubsub.AddPlaybackAsync(ids);
    }
}
