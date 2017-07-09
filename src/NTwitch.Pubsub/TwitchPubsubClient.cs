using NTwitch.Pubsub.API;
using NTwitch.Rest;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NTwitch.Pubsub
{
    public partial class TwitchPubsubClient : BaseTwitchClient, ITwitchClient
    {
        private readonly SemaphoreSlim _stateLock;
        private readonly Logger _pubsubLogger;

        internal new TwitchPubsubApiClient ApiClient => base.ApiClient as TwitchPubsubApiClient;

        /// <summary> Gets the current connection state of this client. </summary>
        public ConnectionState ConnectionState => ApiClient.ConnectionState;
        /// <summary> Gets the estimated round-trip latency, in milliseconds, to the chat server. </summary>
        public int Latency { get; private set; }

        public TwitchPubsubClient() : this(new TwitchPubsubConfig()) { }
        public TwitchPubsubClient(TwitchPubsubConfig config) 
            :base(config, CreateApiClient(config))
        {
            _stateLock = new SemaphoreSlim(1, 1);
            _pubsubLogger = LogManager.CreateLogger("Pubsub");

            ApiClient.SentPubsubMessage += async cmd => await _pubsubLogger.DebugAsync($"Sent {cmd}").ConfigureAwait(false);
            ApiClient.ReceivedPubsubEvent += ProcessMessageAsync;
            ApiClient.Disconnected += ex => _disconnectedEvent.InvokeAsync(ex);

            LatencyUpdated += async (old, val) => await _pubsubLogger.InfoAsync($"Latency = {val} ms").ConfigureAwait(false);
        }

        private static TwitchPubsubApiClient CreateApiClient(TwitchPubsubConfig config)
            => new TwitchPubsubApiClient(config.RestClientProvider, config.WebSocketProvider, config.ClientId, TwitchConfig.UserAgent, config.WebSocketHost);

        internal override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DisconnectAsync().GetAwaiter().GetResult();
                ApiClient.Dispose();
            }
        }

        public async Task ConnectAsync()
        {
            await _stateLock.WaitAsync().ConfigureAwait(false);
            try
            {
                await ApiClient.ConnectAsync().ConfigureAwait(false);
                await _connectedEvent.InvokeAsync();
            }
            finally { _stateLock.Release(); }
        }

        public async Task DisconnectAsync()
        {
            await _stateLock.WaitAsync().ConfigureAwait(false);
            try
            {
                await ApiClient.DisconnectAsync().ConfigureAwait(false);
            }
            finally { _stateLock.Release(); }
        }

        internal override async Task OnLogoutAsync()
        {
            await DisconnectAsync().ConfigureAwait(false);
        }

        // General
        public async Task ListenAsync(params string[] topics)
        {
            await ApiClient.ListenAsync(topics, null).ConfigureAwait(false);
        }

        public async Task UnlistenAsync(params string[] topics)
        {
            await ApiClient.UnlistenAsync(topics, null).ConfigureAwait(false);
        }

        // Channels
        public async Task ListenVideoPlaybackAsync(params ulong[] channelIds)
        {
            await ApiClient.ListenVideoPlaybackAsync(channelIds).ConfigureAwait(false);
        }

        // Users
        public async Task ListenWhispersAsync(params ulong[] userIds)
        {
            await ApiClient.ListenWhispersAsync(userIds).ConfigureAwait(false);
        }

        private async Task ProcessMessageAsync(PubsubFrame<string> msg)
        {
            if (msg.Type == "RESPONSE")
            {
                //TODO: Handle Error responses
                return;
            }

            var data = msg.GetData<PubsubInData>();
            string topic = data.Topic.Split('.').First();

            switch (topic)
            {
                //case "channel-bits-events-v1":
                //    break;
                case "channel-subscribe-events-v1":
                    var model = data.GetMessage<Subscription>();
                    await _subscriptionReceived.InvokeAsync(PubsubSubscription.Create(this, model));
                    break;
                //case "whispers":
                //    break;
                default:
                    await _anonymousReceivedEvent.InvokeAsync(data.Message);
                    break;
            }
        }
    }
}
