// 
// This example shows how to subscribe to anonymous topics
// 

using NTwitch;
using NTwitch.Pubsub;
using System;
using System.Threading.Tasks;

namespace Advanced
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
            _client.AnonymousReceived += OnAnonymousReceivedAsync;

            Console.Write("Please enter your oauth token: ");
            string token = Console.ReadLine();

            await _client.LoginAsync(token);
            await _client.StartAsync();
            await Task.Delay(-1);
        }

        private async Task OnConnectedAsync()
        {
            Console.WriteLine();
            Console.Write("Please enter a topic you would like to subscribe to: ");
            string topic = Console.ReadLine();

            await _client.ListenAsync(topic);
        }

        private async Task OnAnonymousReceivedAsync(string json)
        {
            await Console.Out.WriteLineAsync(json);
        }

        private Task OnLogAsync(LogMessage msg)
            => Console.Out.WriteLineAsync(msg.ToString());
    }
}