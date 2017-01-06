using NTwitch.Rest;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch.Test
{
    public class Program
    {
        public static void Main(string[] args)
            => new Program().Start().GetAwaiter().GetResult();

        private TwitchRestClient _client;

        public async Task Start()
        {
            string clientid = "";

            _client = new TwitchRestClient(new TwitchRestConfig()
            {
                LogLevel = LogLevel.Debug
            });

            _client.Log += (l) => Task.Run(() =>
            {
                Console.WriteLine(l);
            });

            await _client.LoginAsync(clientid);
            var channels = await _client.FindChannelsAsync("Overwatch", new TwitchPageOptions(50));
            Console.WriteLine(channels.Count());

            //var properties = channel.GetType().GetTypeInfo().GetProperties();
            //foreach (var p in properties)
            //    Console.WriteLine(p.GetValue(channel));
            
            Console.ReadKey();
        }
    }
}
