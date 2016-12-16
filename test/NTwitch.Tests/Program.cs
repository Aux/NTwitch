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

            await _client.LoginAsync(clientid);

            while (true)
            {
                Console.Write($"Find User: ");
                string search = Console.ReadLine();
                var user = await _client.GetUserAsync(search);

                var properties = user.GetType().GetProperties();

                foreach (var p in properties)
                {
                    Console.WriteLine($"{p.Name}: {p.GetValue(user)}");
                }
            }
        }
    }
}
