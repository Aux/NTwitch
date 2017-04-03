using NTwitch.Pubsub;
using System;
using System.Threading.Tasks;

namespace Advanced
{
    class Program
    {
        public static void Main(string[] args)
            => new Program().StartAsync().GetAwaiter().GetResult();

        private TwitchPubsubClient _client;

        public async Task StartAsync()
        {
            await Task.Delay(0);
        }
    }
}