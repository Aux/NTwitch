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
                MessageCacheSize = 1000
            });

            _client.Log += OnLogAsync;
            _client.Connected += OnConnectedAsync;
            _client.MessageReceived += OnMessageReceivedAsync;
            _client.CurrentUserJoined += OnCurrentUserJoinedAsync;

            await _client.LoginAsync(token);
            await _client.StartAsync();
            await Task.Delay(-1);
        }

        private async Task OnConnectedAsync()
        {
            await _client.JoinChannelAsync("timthetatman");
            await _client.JoinChannelAsync("mymisterfruit");
            await _client.JoinChannelAsync("kephrii");
        }

        private Task OnCurrentUserJoinedAsync(Cacheable<string, ChatSimpleChannel> channel)
        {
            return Console.Out.WriteLineAsync($"Joined channel `{channel.Key}`");
        }

        private async Task OnMessageReceivedAsync(ChatMessage msg)
        {
            //if (msg is ChatNoticeMessage notice)
            //    await Console.Out.WriteLineAsync($"[{notice.Channel.Name}] {notice.SystemMessage}");
            //else
            //    await Console.Out.WriteLineAsync($"[{msg.Channel.Name}] {msg.User.DisplayName ?? msg.User.Name}: {msg.Content}");

            await Console.Out.WriteLineAsync($"({msg.Channel.Messages.Count}) messages in cache for `{msg.Channel.Name}`");
        }

        //private Task OnUserBannedAsync(ChatSimpleChannel channel, ChatSimpleUser user, BanOptions ban)
        //{
        //    return Console.Out.WriteLineAsync($"`{user.DisplayName}` was banned in `{channel.Name}` for `{ban.Reason} ({ban.Duration})`.");
        //}

        private Task OnLogAsync(LogMessage msg)
        {
            return Console.Out.WriteLineAsync(msg.ToString());
        }
    }
}