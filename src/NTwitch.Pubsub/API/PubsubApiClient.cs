using NTwitch.Pubsub.API;
using NTwitch.Rest;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch.Pubsub
{
    public partial class PubsubApiClient
    {
        private SocketClient _client;

        internal ConcurrentDictionary<string, PubsubRequest> Requests;
        internal LogManager Logger;
        
        public PubsubApiClient(TwitchPubsubConfig config)
        {
            Logger = new LogManager(config.LogLevel);

            if (config.PubsubProvider == null)
                _client = new WebSocketClient(Logger, config.PubsubHost);
            else
                _client = config.PubsubProvider;

            _client.MessageReceived += OnMessageInternalAsync;
        }

        private async Task OnMessageInternalAsync(string msg)
        {
            var response = PubsubResponse.FromString(msg);

            if (!Requests.TryRemove(response.Nonce, out PubsubRequest request))
                await Logger.DebugAsync("Pubsub", $"Received {response.Type} event with no matching nonce.").ConfigureAwait(false);
            
            switch (response.Type.ToLower())
            {
                case "pong":
                    await latencyUpdatedEvent.InvokeAsync(request.GetTime()).ConfigureAwait(false);
                    break;
                case "reconnect": // Reconnecting event
                    await Logger.DebugAsync("Pubsub", $"Recieved reconnect request").ConfigureAwait(false);
                    break;
                case "response":
                    await Logger.DebugAsync("Pubsub", $"Received {request.Nonce} after {request.GetTime()}ms").ConfigureAwait(false);
                    break;
                case "message":
                    await messageReceivedEvent.InvokeAsync(response.Data.Topic, response.Data.Message).ConfigureAwait(false);
                    break;
                default:
                    await Logger.ErrorAsync("Pubsub", $"Recieved unknown message type `{response.Type}`").ConfigureAwait(false);
                    break;
            }
        }

        public Task SendAsync(string type)
            => SendAsync(new PubsubRequest(type));
        public Task SendAsync(string type, params string[] topics)
            => SendAsync(new PubsubRequest(type).WithData(null, topics));
        public async Task SendAsync(PubsubRequest request)
        {
            if (!Requests.TryAdd(request.Nonce, request))
                throw new Exception($"Unable to create nonce for {request.Type} {request.Data?.Topics.First()}");

            await _client.SendAsync(request.ToString());
        }
        
        #region Channels

        internal Task AddPlaybackAsync(ulong[] ids)
        {
            var topics = ids.Select(x => $"video-playback.{x}");
            var request = new PubsubRequest("LISTEN")
                .WithData(null, topics.ToArray());

            return SendAsync(request);
        }

        #endregion
        #region Chat
        
        internal Task AddBitsReceivedAsync(RestTokenInfo info)
        {
            string topic = $"whispers.{info.UserId}";
            var request = new PubsubRequest("LISTEN")
                .WithData(info.Token, topic);

            return SendAsync(request);
        }

        internal Task RemoveBitsReceivedAsync(RestTokenInfo info)
        {
            string topic = $"whispers.{info.UserId}";
            var request = new PubsubRequest("UNLISTEN")
                .WithData(info.Token, topic);

            return SendAsync(request);
        }

        internal Task AddWhisperAsync(RestTokenInfo info)
        {
            if (!info.Authorization?.Scopes.Contains("chat_login") ?? false)
                throw new MissingScopeException("chat_login");

            string topic = $"whispers.{info.UserId}";
            var request = new PubsubRequest("LISTEN")
                .WithData(info.Token, topic);

            return SendAsync(request);
        }

        internal Task RemoveWhisperAsync(RestTokenInfo info)
        {
            if (!info.Authorization?.Scopes.Contains("chat_login") ?? false)
                throw new MissingScopeException("chat_login");

            string topic = $"whispers.{info.UserId}";
            var request = new PubsubRequest("UNLISTEN")
                .WithData(info.Token, topic);

            return SendAsync(request);
        }

        #endregion
    }
}
