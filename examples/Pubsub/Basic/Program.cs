// 
// This example shows how to subscribe to bits for the current user.
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
            _client.Connected += OnConnectedAsync;
            _client.BitsReceived += OnBitsReceivedAsync;

            Console.Write("Please enter your oauth token: ");
            string token = Console.ReadLine();

            await _client.LoginAsync(token);
            await _client.StartAsync();
            await Task.Delay(-1);
        }

        private async Task OnConnectedAsync()
        {
            await _client.ListenBitsAsync(_client.TokenInfo.UserId);
        }

        private Task OnBitsReceivedAsync(PubsubBitsMessage msg)
        {
            return Console.Out.WriteLineAsync($"{msg.User.Name} sent {msg.BitsUsed} bits to {msg.Channel.Name}");
        }

        private Task OnLogAsync(LogMessage msg)
            => Console.Out.WriteLineAsync(msg.ToString());
    }
}