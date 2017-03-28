using NTwitch.Pubsub;
using System;
using System.Threading.Tasks;

namespace NTwitch.Tests
{
    class Program
    {
        public static void Main(string[] args)
            => new Program().Start().GetAwaiter().GetResult();

        private TwitchPubsubClient _client;

        public async Task Start()
        {
            var client = new SocketClient(new TwitchPubsubConfig());

            client.MessageReceived += OnMessageReceived;
            client.Connected += OnConnected;
            client.Disconnected += OnDisconnected;
            
            await client.ConnectAsync();

            await client.SendAsync("{\"type\":\"PING\"}");
            await client.SendAsync("{\"type\":\"PING\"}");

            await Task.Delay(-1);
        }

        private Task OnConnected()
        {
            return Console.Out.WriteLineAsync("Connected");
        }

        private Task OnDisconnected()
        {
            return Console.Out.WriteLineAsync("Disconnected");
        }

        private Task OnMessageReceived(string arg)
        {
            return Console.Out.WriteLineAsync(arg);
        }

        private Task OnLogAsync(LogMessage msg)
        {
            Console.WriteLine(msg);
            return Task.CompletedTask;
        }
    }
}