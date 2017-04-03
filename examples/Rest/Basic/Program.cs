using NTwitch;
using NTwitch.Rest;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Basic
{
    class Program
    {
        public static void Main(string[] args)
            => new Program().StartAsync().GetAwaiter().GetResult();

        private TwitchRestClient _client;

        public async Task StartAsync()
        {
            Console.Write("Please enter your client id: ");
            string clientid = Console.ReadLine();

            _client = new TwitchRestClient(new TwitchRestConfig()
            {
                ClientId = clientid,
                LogLevel = LogLevel.Info
            });

            _client.Log += OnLogAsync;

            while (true)
            {
                Console.WriteLine();
                Console.Write("Enter the name of a stream: ");
                string name = Console.ReadLine();

                var user = (await _client.GetUsersAsync(name)).FirstOrDefault();
                if (user == null)
                {
                    Console.WriteLine($"The user `{name}` does not exist!");
                    continue;
                }

                var stream = await _client.GetStreamAsync(user.Id);
                if (stream == null)
                    Console.WriteLine($"{user.DisplayName} is not currently streaming.");
                else
                    Console.WriteLine($"{user.DisplayName} is streaming {stream.Game} at {stream.Channel.Url}!");
            }
        }

        private Task OnLogAsync(LogMessage msg)
            => Console.Out.WriteLineAsync(msg.ToString());
    }
}