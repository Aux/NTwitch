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
            string token = "";
            string clientId = "";

            try
            {
                _client = new TwitchRestClient(new TwitchRestConfig()
                {
                    ClientId = clientId,
                    LogLevel = LogSeverity.Debug
                });

                _client.Log += OnLogAsync;

                await _client.LoginAsync(token);
                var user = await _client.GetCurrentUserAsync();
                var users = await _client.GetUsersAsync("wraxu");

                Console.WriteLine($"Current Token: {_client.TokenInfo?.Username}");
                Console.WriteLine($"Current User: {user?.DisplayName}");
                Console.WriteLine($"Got User: {users.First().DisplayName} ({user.Id})");

            } catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            await Task.Delay(-1);
        }

        private Task OnLogAsync(LogMessage msg)
        {
            return Console.Out.WriteLineAsync(msg.ToString());
        }
    }
}