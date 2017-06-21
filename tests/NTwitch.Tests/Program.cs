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
            
            try
            {
                _client = new TwitchChatClient(new TwitchChatConfig
                {
                    ClientId = clientId,
                    LogLevel = LogSeverity.Debug
                });

                _client.Log += OnLogAsync;
                _client.MessageReceived += OnMessageReceivedAsync;
                _client.UserBanned += OnUserBannedAsync;

                await _client.LoginAsync(token);
                await _client.ConnectAsync();

                await _client.JoinChannelAsync("timthetatman");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            await Task.Delay(-1);
        }

        private Task OnMessageReceivedAsync(ChatMessage msg)
        {
            return Console.Out.WriteLineAsync($"[{msg.Channel.Name}] {msg.User.DisplayName ?? msg.User.Name}: {msg.Content}");
        }

        private Task OnUserBannedAsync(ChatSimpleChannel channel, ChatSimpleUser user, BanOptions ban)
        {
            return Console.Out.WriteLineAsync($"`{user.DisplayName}` was banned in `{channel.Name}` for `{ban.Reason} ({ban.Duration})`.");
        }

        private Task OnLogAsync(LogMessage msg)
        {
            return Console.Out.WriteLineAsync(msg.ToString());
        }
    }
}