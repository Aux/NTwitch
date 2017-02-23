using Newtonsoft.Json;
using NTwitch.Pubsub;
using System;
using System.Threading.Tasks;

namespace NTwitch.Tests
{
    class Program
    {
        static void Main(string[] args)
            => new Program().StartAsync().GetAwaiter().GetResult();

        private TwitchPubsubClient _client;

        public async Task StartAsync()
        {
            _client = new TwitchPubsubClient(new TwitchPubsubConfig()
            {
                LogLevel = LogLevel.Debug
            });

            _client.Log += OnLog;
          
            await _client.ConnectAsync();

            await _client.SubscribeAsync(new PubsubTopic("video-playback", 42481140), x =>
            {
                Console.WriteLine(x.Data.Topic);
                return Task.CompletedTask;
            });

            await Task.Delay(-1);
        }
        
        private Task OnLog(LogMessage msg)
        {
            Console.WriteLine($"[{msg.Level}] {msg.Source}: {msg.Message}");
            return Task.CompletedTask;
        }
    }
}