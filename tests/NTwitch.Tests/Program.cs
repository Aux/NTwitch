using NTwitch.Chat;
using NTwitch.Pubsub;
using System;
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
                    LogLevel = LogSeverity.Debug,
                    SocketClientProvider = DefaultWebSocketProvider.Instance,
                    SocketHost = "wss://irc-ws.chat.twitch.tv"
                });

                _client.Log += OnLogAsync;
                _client.MessageReceived += OnMessageReceivedAsync;

                await _client.LoginAsync(token);
                await _client.ConnectAsync();
                
                await _client.JoinChannelAsync("wraxu");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            await Task.Delay(-1);
        }

        private Task OnMessageReceivedAsync(ChatMessage msg)
        {
            return Console.Out.WriteLineAsync($"[{msg.Channel.Name}] {msg.User.DisplayName}: {msg.Content}");
        }

        private Task OnLogAsync(LogMessage msg)
        {
            return Console.Out.WriteLineAsync(msg.ToString());
        }
    }
}