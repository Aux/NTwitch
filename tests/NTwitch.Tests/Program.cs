using System;
using System.Threading.Tasks;
using NTwitch.Helix.Rest;

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

            _client = new TwitchRestClient(new TwitchRestConfig
            {
                ClientId = clientId,
                LogLevel = Helix.LogSeverity.Debug
            });

            await _client.LoginAsync(token);

            var user = await _client.GetUsersAsync("auxesistv");

            await Task.Delay(-1);
        }
        
        private Task OnLogAsync(LogMessage msg)
        {
            return Console.Out.WriteLineAsync(msg.ToString());
        }
    }
}