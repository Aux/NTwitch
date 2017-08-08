using NTwitch.Chat;
using NTwitch.Pubsub;
using NTwitch.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch.Tests
{
    class Program
    {
        public static void Main(string[] args)
            => new Program().Start().GetAwaiter().GetResult();

        private TwitchChatClient _client;

        public async Task Start()
        {
            string token = "";
            string clientId = "";

            _client = new TwitchChatClient(new TwitchChatConfig
            {
                ClientId = clientId,
                LogLevel = LogSeverity.Debug,
                MessageCacheSize = 100,
                SocketClientProvider = DefaultWebSocketProvider.Instance,
                SocketHost = "wss://irc-ws.chat.twitch.tv"
            });

            _client.Log += OnLogAsync;
            _client.Connected += OnConnectedAsync;
            _client.MessageReceived += OnMessageReceivedAsync;
            _client.UserJoined += OnUserJoinedAsync;
            _client.UserLeft += OnUserLeftAsync;

            await _client.LoginAsync(token);
            await _client.StartAsync();
            await Task.Delay(-1);
        }

        private Task OnUserJoinedAsync(Cacheable<string, ChatSimpleChannel> channel, Cacheable<string, ChatSimpleUser> user)
            => Console.Out.WriteLineAsync($"`{user.Key}` joined `{channel.Key}`");

        private Task OnUserLeftAsync(Cacheable<string, ChatSimpleChannel> channel, Cacheable<string, ChatSimpleUser> user)
            => Console.Out.WriteLineAsync($"`{user.Key}` left `{channel.Key}`");

        private async Task OnConnectedAsync()
        {
            await _client.JoinChannelAsync("emongg");
        }
        
        private async Task OnMessageReceivedAsync(ChatMessage msg)
        {
            if (msg is ChatNoticeMessage notice)
                await Console.Out.WriteLineAsync($"[{notice.Channel.Name}] {notice.SystemMessage}");
            else
                await Console.Out.WriteLineAsync($"[{msg.Channel.Name}] {msg.Author.DisplayName ?? msg.Author.Name}: {msg.Content}");
        }
        
        private Task OnLogAsync(LogMessage msg)
        {
            return Console.Out.WriteLineAsync(msg.ToString());
        }
    }
}