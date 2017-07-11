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

        private TwitchPubsubClient _client;

        public async Task Start()
        {
            string token = "";
            string clientId = "";

            _client = new TwitchPubsubClient(new TwitchPubsubConfig
            {
                ClientId = clientId,
                LogLevel = LogSeverity.Debug
            });

            _client.Log += OnLogAsync;
            _client.Connected += OnConnectedAsync;
            _client.AnonymousReceived += OnAnonymousReceivedAsync;

            await _client.LoginAsync(token);
            await _client.StartAsync();
            await Task.Delay(-1);
        }

        private async Task OnConnectedAsync()
        {
            await _client.ListenAsync($"video-playback.{_client.TokenInfo.UserId}");
        }

        private Task OnAnonymousReceivedAsync(string json)
        {
            return Console.Out.WriteLineAsync(json);
        }
        
        //private Task OnMessageReceivedAsync(ChatMessage msg)
        //{
        //    if (msg is ChatNoticeMessage notice)
        //        return Console.Out.WriteLineAsync($"[{notice.Channel.Name}] {notice.SystemMessage}");
        //    else 
        //        return Console.Out.WriteLineAsync($"[{msg.Channel.Name}] {msg.User.DisplayName ?? msg.User.Name}: {msg.Content}");
        //}

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