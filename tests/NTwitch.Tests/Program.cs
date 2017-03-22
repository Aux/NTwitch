using Newtonsoft.Json;
using NTwitch.Rest;
using System;
using System.Linq;
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
            _client = new TwitchRestClient(new TwitchRestConfig()
            {
                LogLevel = LogLevel.Debug
            });

            _client.Log += OnLogAsync;

            await _client.LoginAsync(TokenType.Oauth, "");

            var users = await _client.GetUsersAsync("auxesistv");
            var channel = await users.First().GetChannelAsync();

            Console.WriteLine($"Before: {channel.Status}");

            await channel.ModifyAsync(x =>
            {
                x.Status = "NTwitch changed this status :D";
            });

            Console.WriteLine($"After: {channel.Status}");
            
            await Task.Delay(-1);
        }

        private Task OnLogAsync(LogMessage msg)
        {
            Console.WriteLine(msg);
            return Task.CompletedTask;
        }
    }
}