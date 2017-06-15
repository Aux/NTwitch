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

                //var streams = await _client.GetStreamsAsync(x => { x.Game = "Overwatch"; });
                //foreach (var stream in streams)
                //    await _client.JoinChannelAsync(stream.Channel.Name);
                
                await _client.JoinChannelAsync("auxesistv");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            await Task.Delay(-1);
        }

        private async Task OnMessageReceivedAsync(ChatMessage msg)
        {
            if (msg.Content == "!count")
            {
                var messages = msg.Channel.GetMessages();
                await msg.Channel.SendMessageAsync($"I currently have {messages.Count} message(s) cached for this channel");
            }

            await Console.Out.WriteLineAsync($"[{msg.Channel.Name}] {msg.User.Name}: {msg.Content}");
        }

        private Task OnLogAsync(LogMessage msg)
        {
            return Console.Out.WriteLineAsync(msg.ToString());
        }
    }
}