using Newtonsoft.Json;
using NTwitch.Chat.API;
using NTwitch.Chat.Queue;
using NTwitch.Rest;
using NTwitch.Rest.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NTwitch.Chat
{
    internal class TwitchChatApiClient : TwitchRestApiClient
    {
        public event Func<string, Task> SentChatMessage { add { _sentChatMessageEvent.Add(value); } remove { _sentChatMessageEvent.Remove(value); } }
        private readonly AsyncEvent<Func<string, Task>> _sentChatMessageEvent = new AsyncEvent<Func<string, Task>>();
        public event Func<ChatResponse, Task> ReceivedChatEvent { add { _receivedChatEvent.Add(value); } remove { _receivedChatEvent.Remove(value); } }
        private readonly AsyncEvent<Func<ChatResponse, Task>> _receivedChatEvent = new AsyncEvent<Func<ChatResponse, Task>>();

        public event Func<Exception, Task> Disconnected { add { _disconnectedEvent.Add(value); } remove { _disconnectedEvent.Remove(value); } }
        private readonly AsyncEvent<Func<Exception, Task>> _disconnectedEvent = new AsyncEvent<Func<Exception, Task>>();

        private CancellationTokenSource _connectCancelToken;
        private string _socketUrl;

        internal ISocketClient SocketClient { get; }
        public ConnectionState ConnectionState { get; private set; }

        public TwitchChatApiClient(RestClientProvider restClientProvider, SocketClientProvider socketClientProvider, 
            string clientId, string userAgent, string socketUrl, RetryMode defaultRetryMode = RetryMode.AlwaysRetry, JsonSerializer serializer = null)
            : base(restClientProvider, clientId, userAgent, serializer)
        {
            _socketUrl = socketUrl;
            SocketClient = socketClientProvider();
            
            SocketClient.TextMessage += async text =>
            {
                var msg = ChatResponse.Parse(text);
                await _receivedChatEvent.InvokeAsync(msg).ConfigureAwait(false);
            };
            SocketClient.Closed += async ex =>
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
                    (SocketClient as IDisposable)?.Dispose();
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
                if (SocketClient != null)
                    SocketClient.SetCancelToken(_connectCancelToken.Token);

                await SocketClient.ConnectAsync(_socketUrl).ConfigureAwait(false);
                ConnectionState = ConnectionState.Connected;
            } catch
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

            await SocketClient.DisconnectAsync().ConfigureAwait(false);

            ConnectionState = ConnectionState.Disconnected;
        }

        public Task SendSocketAsync(ChatRequestBuilder builder, RequestOptions options = null)
            => SendSocketAsync(builder.Command, builder.Parameters, builder.GetTagString(), options);
        public async Task SendSocketAsync(string command, string parameters, string tags = null, RequestOptions options = null)
        {
            CheckLoginState();
            options = options ?? new RequestOptions();

            var request = new ChatRequest(SocketClient, $"{tags}{command} {parameters}".Trim(), options);
            await request.SendAsync().ConfigureAwait(false);
            await _sentChatMessageEvent.InvokeAsync(command).ConfigureAwait(false);
        }

        // Authorization
        public async Task SendIdentifyAsync(string username, RequestOptions options)
        {
            options = RequestOptions.CreateOrClone(options);
            await SendSocketAsync("PASS", $"oauth:{AuthToken}").ConfigureAwait(false);
            await SendSocketAsync("NICK", $"{username}").ConfigureAwait(false);
        }

        public async Task SendPingAsync(RequestOptions options)
        {
            options = RequestOptions.CreateOrClone(options);
            await SendSocketAsync("PING", "").ConfigureAwait(false);
        }

        public async Task RequestTagsAsync(RequestOptions options)
        {
            options = RequestOptions.CreateOrClone(options);
            await SendSocketAsync("CAP REQ", ":twitch.tv/tags").ConfigureAwait(false);
        }

        public async Task RequestMembershipAsync(RequestOptions options)
        {
            options = RequestOptions.CreateOrClone(options);
            await SendSocketAsync("CAP REQ", ":twitch.tv/membership").ConfigureAwait(false);
        }

        public async Task RequestCommandsAsync(RequestOptions options)
        {
            options = RequestOptions.CreateOrClone(options);
            await SendSocketAsync("CAP REQ", ":twitch.tv/commands").ConfigureAwait(false);
        }

        // Channels
        public async Task JoinChannelAsync(string channelName, RequestOptions options)
        {
            options = RequestOptions.CreateOrClone(options);
            await SendSocketAsync("JOIN", $"#{channelName}").ConfigureAwait(false);
        }

        public async Task LeaveChannelAsync(string channelName, RequestOptions options)
        {
            options = RequestOptions.CreateOrClone(options);
            await SendSocketAsync("PART", $"#{channelName}").ConfigureAwait(false);
        }

        public async Task SendChannelMessageAsync(string channelName, string content, RequestOptions options)
        {
            options = RequestOptions.CreateOrClone(options);
            await SendSocketAsync("PRIVMSG", $"#{channelName} :{content}");
        }

        public async Task SetUserModeAsync(string channelName, string userName, bool isOp, RequestOptions options)
        {
            options = RequestOptions.CreateOrClone(options);
            await SendSocketAsync(new SetUserModeRequest(channelName, userName, isOp)).ConfigureAwait(false);
        }

        public Task RequestNamesAsync(string channelName, RequestOptions options)
        {
            throw new NotImplementedException();
        }

        public async Task ClearChatAsync(string channelName, string userName, string reason, uint? duration, RequestOptions options)
        {
            options = RequestOptions.CreateOrClone(options);
            await SendSocketAsync(new ClearChatRequest(channelName, userName, reason, duration), options).ConfigureAwait(false);
        }

        public async Task HostChannelAsync(string hostName, string channelName, RequestOptions options)
        {
            options = RequestOptions.CreateOrClone(options);
            await SendSocketAsync("HOSTTARGET", $"#{hostName} {channelName}").ConfigureAwait(false);
        }
    }
}
