using NTwitch.Pubsub.API;
using NTwitch.Rest;
using System;
using System.Collections.Generic;
using System.Text;
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
        
        internal async Task TryConnectAsync()
        {
            if (ConnectionState == ConnectionState.Disconnected)
                await ApiClient.ConnectAsync().ConfigureAwait(false);
        }

        public async Task DisconnectAsync()
            => await ApiClient.DisconnectAsync().ConfigureAwait(false);

        internal override async Task OnLogoutAsync()
        {
            await DisconnectAsync().ConfigureAwait(false);
        }

        // General
        public Task SubscribeAsync(string topic)
        {
            throw new NotImplementedException();
        }

        public Task UnsubscribeAsync(string topic)
        {
            throw new NotImplementedException();
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

        private Task ProcessMessageAsync(PubsubFrame<PubsubInData> msg)
        {
            return Console.Out.WriteLineAsync(Newtonsoft.Json.JsonConvert.SerializeObject(msg));
        }
    }
}
