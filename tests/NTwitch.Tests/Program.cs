using NTwitch.Chat;
using NTwitch.Pubsub;
using NTwitch.Rest;
using System;
using System.Threading.Tasks;

namespace NTwitch.Tests
{
    class Program
    {
        public static void Main(string[] args)
            => new Program().Start().GetAwaiter().GetResult();

        private TwitchRestClient _client;

        public async Task Start()
        {
            string token = "";
            string clientId = "";

            try
            {
                _client = new TwitchRestClient(new TwitchRestConfig
                {
                    ClientId = clientId,
                    LogLevel = LogSeverity.Debug,
                });

                _client.Log += OnLogAsync;
                
                await _client.LoginAsync(token);
                var user = await _client.GetCurrentUserAsync();
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