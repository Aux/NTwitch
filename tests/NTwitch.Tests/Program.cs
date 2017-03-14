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
            _client = new TwitchRestClient();
            await _client.LoginAsync(TokenType.Oauth, "");
            var users = await _client.GetUsersAsync("auxesistv", "emongg", "timthetatman");

            Console.WriteLine($"Got users: {string.Join(", ", users.Select(x => $"{x.DisplayName} ({x.Id})"))}");
            
            await Task.Delay(-1);
        }
    }
}