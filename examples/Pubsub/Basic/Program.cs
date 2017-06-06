// 
// This example is not complete and not compatible with latest NTwitch
// 

using NTwitch;
using NTwitch.Pubsub;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Basic
{
    class Program
    {
        public static void Main(string[] args)
            => new Program().StartAsync().GetAwaiter().GetResult();

        private TwitchPubsubClient _client;

        public async Task StartAsync()
        {
            Console.Write("Please enter your client id: ");
            string clientid = Console.ReadLine();
            
            _client = new TwitchPubsubClient(new TwitchPubsubConfig()
            {
                ClientId = clientid,
                LogLevel = LogSeverity.Info
            });
            
            _client.Log += OnLogAsync;
            _client.StreamOnline += OnStreamOnlineAsync;

            while (true)
            {
                Console.WriteLine();
                Console.Write("Enter the name of a stream to watch for: ");
                string name = Console.ReadLine();

                var user = (await _client.GetUsersAsync(name)).FirstOrDefault();
                if (user == null)
                {
                    Console.WriteLine($"The user `{name}` does not exist!");
                    continue;
                }

                await _client.SubscribePlaybackAsync(user.Id);
            }
        }

        private Task OnStreamOnlineAsync()
        {
            throw new NotImplementedException();
        }

        private Task OnLogAsync(LogMessage msg)
            => Console.Out.WriteLineAsync($"[{msg.Level}] {msg.Source}: {msg.Message}");
    }
}