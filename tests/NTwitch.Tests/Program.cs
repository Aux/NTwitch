using NTwitch.Rest;
using System;
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
            var user = await _client.GetUserAsync("AuxesisTV");
            
            Console.ReadKey();
        }
    }
}
