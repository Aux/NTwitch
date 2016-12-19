using NTwitch.Rest;
using System;
using System.Reflection;
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

            _client = new TwitchRestClient();

            _client.Log += (l) => Task.Run(() =>
            {
                Console.WriteLine(l);
            });

            await _client.LoginAsync(clientid);

            var top = await _client.GetTopGamesAsync(new TwitchPagination(50));

            foreach (var g in top.Games)
                Console.WriteLine($"{g.Game.Name}\n{g.Channels}c\t{g.Viewers}v");
            
            Console.ReadKey();
        }
    }
}
