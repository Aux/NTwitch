using NTwitch.Rest;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace NTwitch.Pubsub
{
    public partial class TwitchPubsubClient : BaseRestClient, ITwitchClient
    {
        private SocketClient _socket = null;
        private ConcurrentDictionary<string, Func<PubsubMessage, Task>> _subscriptions;
        private string _token = null;
        private string _host;
        
        public TwitchPubsubClient() : base(new TwitchPubsubConfig()) { }
        public TwitchPubsubClient(TwitchPubsubConfig config) : base(config)
        {
            _host = config.PubsubUrl;
            _subscriptions = new ConcurrentDictionary<string, Func<PubsubMessage, Task>>();
        }
        
        public async Task LoginAsync(TokenType type, string token)
        {
            _token = token;
            await LoginInternalAsync(type, _token);
        }

        public async Task ConnectAsync()
        {
            _socket = new SocketClient(Logger, _host, _token);
            _socket.EventReceived += OnEventReceivedAsync;
            await _socket.ConnectAsync();
        }
        
        public async Task DisconnectAsync()
        {
            await _socket.DisconnectAsync();
        }

        private Task OnEventReceivedAsync(PubsubMessage msg)
        {
            if (_subscriptions.TryGetValue(msg.Data.Topic, out Func<PubsubMessage, Task> action))
                action.Invoke(msg);

            return Task.CompletedTask;
        }

        public async Task SubscribeAsync(PubsubTopic topic, Func<PubsubMessage, Task> action)
        {
            if (_socket == null)
                await ConnectAsync();

            await _socket.SendAsync("LISTEN", topic.ToString());

            if (!_subscriptions.TryAdd(topic.ToString(), action))
                throw new Exception("Unable to add listen.");
            await Logger.DebugAsync("Pubsub", $"Subscribed to {topic.ToString()}").ConfigureAwait(false);
        }

        public async Task UnsubscribeAsync(PubsubTopic topic)
        {
            if (_socket == null)
                throw new InvalidOperationException("The client is not connected.");

            await _socket.SendAsync("UNLISTEN", topic.ToString());

            if (!_subscriptions.TryRemove(topic.ToString(), out Func<PubsubMessage, Task> value))
                throw new Exception("Unable to remove listen.");
            await Logger.DebugAsync("Pubsub", $"Unsubscribed from {topic.ToString()}").ConfigureAwait(false);
        }
    }
}
