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
            _client = new TwitchRestClient();
            await _client.LoginAsync(TokenType.Oauth, "");
            var community = await _client.GetCommunityAsync("CompetitiveOW", true);
            var owner = await community.GetOwnerAsync();

            Console.WriteLine($"{owner.DisplayName} ({owner.Id})");

            await Task.Delay(-1);
        }
    }
}