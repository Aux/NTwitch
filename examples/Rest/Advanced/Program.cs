using NTwitch.Rest;
using System;
using System.Threading.Tasks;

namespace Advanced
{
    class Program
    {
        public static void Main(string[] args)
            => new Program().StartAsync().GetAwaiter().GetResult();

        private TwitchRestClient _client;

        public async Task StartAsync()
        {
            await Task.Delay(0);
        }
    }
}