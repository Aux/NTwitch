using Newtonsoft.Json;
using NTwitch.Rest;
using System;
using System.Threading.Tasks;

namespace NTwitch.Tests
{
    class Program
    {
        static void Main(string[] args)
            => new Program().StartAsync().GetAwaiter().GetResult();

        private TwitchRestClient _client;

        public async Task StartAsync()
        {
            _client = new TwitchRestClient(new TwitchRestConfig()
            {
                LogLevel = LogLevel.Debug
            });

            _client.Log += OnLog;

            await _client.LoginAsync(TokenType.OAuth, "");
            var user = await _client.FindUserAsync("theonemanny");
            var channel = await _client.GetChannelAsync(user.Id);

            Console.WriteLine(JsonConvert.SerializeObject(clips, Formatting.Indented));

            await Task.Delay(-1);
        }

        private Task OnLog(LogMessage msg)
        {
            Console.WriteLine($"[{msg.Level}] {msg.Source}: {msg.Message}");
            return Task.CompletedTask;
        }
    }
}
