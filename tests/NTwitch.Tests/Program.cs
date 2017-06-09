using NTwitch.Chat;
using NTwitch.Rest;
using System;
using System.Linq;
using System.Threading.Tasks;
using NTwitch.Chat.Queue;

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
                _client = new TwitchChatClient(new TwitchChatConfig()
                {
                    ClientId = clientId,
                    LogLevel = LogSeverity.Debug
                });

                _client.Log += OnLogAsync;
                
                await _client.LoginAsync(token);
                await _client.ConnectAsync();

                await _client.JoinChannelAsync("wraxu");
                await _client.JoinChannelAsync("timthetatman");

            } catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            await Task.Delay(-1);
        }
        
        private Task OnLogAsync(LogMessage msg)
        {
            return Console.Out.WriteLineAsync(msg.ToString());
        }
    }
}