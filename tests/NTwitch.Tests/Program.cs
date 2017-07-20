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
                MessageCacheSize = 100
            });

            _client.Log += OnLogAsync;
            _client.Connected += OnConnectedAsync;
            _client.MessageReceived += OnMessageReceivedAsync;

            await _client.LoginAsync(token);
            await _client.StartAsync();
            await Task.Delay(-1);
        }

        private async Task OnConnectedAsync()
        {
            await _client.JoinChannelAsync("auxesistv");
        }
        
        private async Task OnMessageReceivedAsync(ChatMessage msg)
        {
            if (msg is ChatNoticeMessage notice)
                await Console.Out.WriteLineAsync($"[{notice.Channel.Name}] {notice.SystemMessage}");
            else
                await Console.Out.WriteLineAsync($"[{msg.Channel.Name}] {msg.User.DisplayName ?? msg.User.Name}: {msg.Content}");
        }
        
        private Task OnLogAsync(LogMessage msg)
        {
            return Console.Out.WriteLineAsync(msg.ToString());
        }
    }
}