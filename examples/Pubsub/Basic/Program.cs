// 
// This example shows how to subscribe to whispers for the current user.
// 

using NTwitch;
using NTwitch.Pubsub;
using System;
using System.Threading.Tasks;

namespace Basic
{
    class Program
    {
        public static void Main(string[] args)
            => new Program().StartAsync().GetAwaiter().GetResult();

        private TwitchPubsubClient _client;

        public async Task StartAsync()
        {
            _client = new TwitchPubsubClient(new TwitchPubsubConfig()
            {
                LogLevel = LogSeverity.Info
            });

            _client.Log += OnLogAsync;
            _client.WhisperReceived += OnWhisperReceivedAsync;

            Console.Write("Please enter your oauth token: ");
            string token = Console.ReadLine();

            await _client.LoginAsync(token);
            await _client.ListenWhispersAsync(_client.TokenInfo.UserId);
            
            await Task.Delay(-1);
        }

        private Task OnLogAsync(LogMessage msg)
            => Console.Out.WriteLineAsync(msg.ToString());

        private Task OnWhisperReceivedAsync(string arg)
        {
            return Console.Out.WriteLineAsync(arg);
        }
    }
}