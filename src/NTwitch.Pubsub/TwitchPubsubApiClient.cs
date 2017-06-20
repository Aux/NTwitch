using Newtonsoft.Json;
using NTwitch.Pubsub.API;
using NTwitch.Pubsub.Queue;
using NTwitch.Rest;
using NTwitch.Rest.API;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NTwitch.Pubsub
{
    internal class TwitchPubsubApiClient : TwitchRestApiClient
    {
        public event Func<string, Task> SentPubsubMessage { add { _sentPusbubMessageEvent.Add(value); } remove { _sentPusbubMessageEvent.Remove(value); } }
        private readonly AsyncEvent<Func<string, Task>> _sentPusbubMessageEvent = new AsyncEvent<Func<string, Task>>();
        public event Func<PubsubFrame<string>, Task> ReceivedPubsubEvent { add { _receivedPubsubEvent.Add(value); } remove { _receivedPubsubEvent.Remove(value); } }
        private readonly AsyncEvent<Func<PubsubFrame<string>, Task>> _receivedPubsubEvent = new AsyncEvent<Func<PubsubFrame<string>, Task>>();

        public event Func<Task> Connected { add { _connectedEvent.Add(value); } remove { _connectedEvent.Remove(value); } }
        private readonly AsyncEvent<Func<Task>> _connectedEvent = new AsyncEvent<Func<Task>>();
        public event Func<Exception, Task> Disconnected { add { _disconnectedEvent.Add(value); } remove { _disconnectedEvent.Remove(value); } }
        private readonly AsyncEvent<Func<Exception, Task>> _disconnectedEvent = new AsyncEvent<Func<Exception, Task>>();
        
        private CancellationTokenSource _connectCancelToken;
        private string _webSocketUrl;

        internal IWebSocketClient WebSocketClient { get; }

        public ConnectionState ConnectionState { get; private set; }
        
        public TwitchPubsubApiClient(RestClientProvider restClientProvider, SocketClientProvider socketClientProvider, string clientId, string userAgent,
            string webSocketUrl, RetryMode defaultRetryMode = RetryMode.AlwaysRetry, JsonSerializer serializer = null)
            : base(restClientProvider, clientId, userAgent, serializer)
        {
            _webSocketUrl = webSocketUrl;
            WebSocketClient = socketClientProvider() as IWebSocketClient;

            WebSocketClient.TextMessage += async text =>
            {
                using (var reader = new StringReader(text))
                using (var jsonReader = new JsonTextReader(reader))
                {
                    var msg = _serializer.Deserialize<PubsubFrame<string>>(jsonReader);
                    if (msg != null)
                        await _receivedPubsubEvent.InvokeAsync(msg).ConfigureAwait(false);
                }
            };
            WebSocketClient.Closed += async ex =>
            {
                await DisconnectAsync().ConfigureAwait(false);
                await _disconnectedEvent.InvokeAsync(ex).ConfigureAwait(false);
            };
        }

        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _connectCancelToken?.Dispose();
                    (WebSocketClient as IDisposable)?.Dispose();
                }
                _disposed = true;
            }
        }
        
        public async Task ConnectAsync()
        {
            await _stateLock.WaitAsync().ConfigureAwait(false);
            try
            {
                await ConnectInternalAsync().ConfigureAwait(false);
            }
            finally { _stateLock.Release(); }
        }

        internal override async Task ConnectInternalAsync()
        {
            if (LoginState != LoginState.LoggedIn)
                throw new InvalidOperationException("You must log in before connecting.");

            ConnectionState = ConnectionState.Connecting;
            try
            {
                _connectCancelToken = new CancellationTokenSource();
                if (WebSocketClient != null)
                    WebSocketClient.SetCancelToken(_connectCancelToken.Token);

                await WebSocketClient.ConnectAsync(_webSocketUrl).ConfigureAwait(false);
                ConnectionState = ConnectionState.Connected;
                await _connectedEvent.InvokeAsync().ConfigureAwait(false);
            }
            catch
            {
                await DisconnectInternalAsync().ConfigureAwait(false);
                throw;
            }
        }

        public async Task DisconnectAsync()
        {
            await _stateLock.WaitAsync().ConfigureAwait(false);
            try
            {
                await DisconnectInternalAsync().ConfigureAwait(false);
            }
            finally { _stateLock.Release(); }
        }

        internal override async Task DisconnectInternalAsync()
        {
            if (ConnectionState == ConnectionState.Disconnected) return;
            ConnectionState = ConnectionState.Disconnecting;

            try { _connectCancelToken?.Cancel(false); }
            catch { }

            await WebSocketClient.DisconnectAsync().ConfigureAwait(false);

            ConnectionState = ConnectionState.Disconnected;
        }

        public Task SendSocketAsync(PubsubRequestBuilder builder, RequestOptions options = null)
            => SendSocketAsync(builder.Type, builder.GetPayload(), builder.GetNonce(), options);
        public async Task SendSocketAsync(string type, object payload, string nonce = null, RequestOptions options = null)
        {
            CheckLoginState();
            if (ConnectionState == ConnectionState.Disconnected)
                await ConnectAsync().ConfigureAwait(false);
            
            byte[] bytes = null;
            if (payload != null)
                bytes = Encoding.UTF8.GetBytes(SerializeJson(payload));
            var request = new PubsubRequest(WebSocketClient, null, bytes, true, options);
            await request.SendAsync().ConfigureAwait(false);
            await _sentPusbubMessageEvent.InvokeAsync(type).ConfigureAwait(false);
        }
        
        // General
        public async Task SendPingAsync(RequestOptions options = null)
        {
            options = RequestOptions.CreateOrClone(options);
            await SendSocketAsync(new PubsubRequestBuilder("PING", includeNonce: false), options).ConfigureAwait(false);
        }

        public async Task ListenAsync(string[] topics, RequestOptions options = null)
        {
            options = RequestOptions.CreateOrClone(options);
            var builder = new PubsubRequestBuilder("LISTEN", AuthToken);
            builder.Topics.AddRange(topics);
            await SendSocketAsync(builder, options).ConfigureAwait(false);
        }

        public async Task UnlistenAsync(string[] topics, RequestOptions options = null)
        {
            options = RequestOptions.CreateOrClone(options);
            var builder = new PubsubRequestBuilder("UNLISTEN", AuthToken);
            builder.Topics.AddRange(topics);
            await SendSocketAsync(builder, options).ConfigureAwait(false);
        }

        // Channels
        public async Task ListenBitsAsync(ulong[] channelIds, RequestOptions options = null)
        {
            options = RequestOptions.CreateOrClone(options);
            await SendSocketAsync(new ListenBitsRequest(channelIds, AuthToken), options).ConfigureAwait(false);
        }

        public async Task ListenSubscriptionsAsync(ulong[] channelIds, RequestOptions options = null)
        {
            options = RequestOptions.CreateOrClone(options);
            await SendSocketAsync(new ListenSubscriptionsRequest(channelIds, AuthToken), options).ConfigureAwait(false);
        }

        public async Task ListenVideoPlaybackAsync(ulong[] channelIds, RequestOptions options = null)
        {
            options = RequestOptions.CreateOrClone(options);
            await SendSocketAsync(new ListenVideoPlaybackRequest(channelIds, AuthToken), options).ConfigureAwait(false);
        }

        // Chat
        public async Task ListenWhispersAsync(ulong[] userIds, RequestOptions options = null)
        {
            options = RequestOptions.CreateOrClone(options);
            await SendSocketAsync(new ListenWhispersRequest(userIds, AuthToken), options).ConfigureAwait(false);
        }
    }
}
