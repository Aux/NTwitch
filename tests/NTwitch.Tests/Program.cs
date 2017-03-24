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

            await _client.LoginAsync(AuthMode.Oauth, "");

            var community = await _client.GetCommunityAsync("CompetitiveOW", true);

            if (community == null)
                Console.WriteLine("Could not find a community named `CompetitiveOW`.");
            else
            {
                var owner = await community.GetOwnerAsync();

                Console.WriteLine(JsonConvert.SerializeObject(owner));
            }

            await Task.Delay(-1);
        }

        private Task OnLogAsync(LogMessage msg)
        {
            Console.WriteLine(msg);
            return Task.CompletedTask;
        }
    }
}