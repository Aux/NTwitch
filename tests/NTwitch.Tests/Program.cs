using NTwitch.Chat;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch.Test
{
    public class Program
    {
        public static void Main(string[] args)
            => new Program().Start().GetAwaiter().GetResult();

        private TwitchChatClient _client;

        public async Task Start()
        {
            string token = "";

            _client = new TwitchChatClient(new TwitchChatConfig()
            {
                LogLevel = LogLevel.Debug
            });

            _client.Log += (l) => Task.Run(() =>
            {
                Console.WriteLine(l);
            });

            await _client.ConnectAsync();
            await _client.LoginAsync("auxesistv", token);
            await _client.JoinChannelAsync("gamesdonequick");

            _client.MessageReceived += OnMessageReceived;
            Console.ReadKey();
        }

        private Task OnMessageReceived(ChatMessage msg)
        {
            Console.WriteLine($"[{msg.Channel.Name}] {msg.User.DisplayName}: {msg.Content}");
            return Task.CompletedTask;
        }
    }
}
