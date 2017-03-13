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
            Console.WriteLine("Created client");
            await _client.LoginAsync(TokenType.Oauth, "");
            Console.WriteLine("Logged in");
            var user = await _client.GetUserAsync(118930881);
            Console.WriteLine("Got user: " + user.DisplayName);

            await Task.Delay(-1);
        }
    }
}