// 
// This examples shows how to get a user's channel from an oauth token and edit their status.
// 

using NTwitch;
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
            _client = new TwitchRestClient(new TwitchRestConfig()
            {
                LogLevel = LogSeverity.Info
            });

            _client.Log += OnLogAsync;

            Console.Write("Please enter your oauth token: ");
            string token = Console.ReadLine();

            await _client.LoginAsync(token);
            var channel = await _client.GetChannelAsync(_client.TokenInfo.UserId);

            string previous = channel.Status;
            while (true)
            {
                Console.WriteLine();
                Console.Write("Please enter a new value for the stream title: ");
                string title = Console.ReadLine();

                await channel.ModifyAsync(x =>
                {
                    x.Status = title;
                });

                Console.WriteLine($"I changed {channel.DisplayName}'s status from `{previous}` to `{channel.Status}`");
                previous = channel.Status;
            }
        }

        private Task OnLogAsync(LogMessage msg)
            => Console.Out.WriteLineAsync(msg.ToString());
    }
}