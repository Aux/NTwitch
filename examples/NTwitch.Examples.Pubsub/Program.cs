// 
// This example is incomplete and not compatible with latest NTwitch
// 

using NTwitch.Pubsub;
using System;
using System.Threading.Tasks;

namespace NTwitch.Examples.Pubsub
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
                LogLevel = LogLevel.Info
            });

            Console.Write("Please enter your oauth token: ");
            string token = Console.ReadLine();

            _client.Log += OnLogAsync;
            
            await _client.LoginAsync(AuthMode.Oauth, token);
            await _client.SubscribeAsync("video-playback", _client.Token.UserId.ToString(), OnVideoPlayBackAsync);
        }

        private Task OnVideoPlayBackAsync(string message)
        {
            throw new NotImplementedException();
        }

        private Task OnLogAsync(LogMessage msg)
            => Console.Out.WriteLineAsync($"[{msg.Level}] {msg.Source}: {msg.Message}");
    }
}