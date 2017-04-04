using NTwitch.Chat;
using System;
using System.Threading.Tasks;

namespace NTwitch.Tests
{
    class Program
    {
        public static void Main(string[] args)
            => new Program().Start().GetAwaiter().GetResult();

        private TwitchChatClient _client;

        public async Task Start()
        {
            _client = new TwitchChatClient(new TwitchChatConfig()
            {
                LogLevel = LogLevel.Info
            });

            _client.Log += OnLogAsync;

            await _client.LoginAsync("z05gs6s3y58vxv9gbxv7n8vazk0n98");
            await _client.ConnectAsync();

            await _client.JoinChannelAsync("timthetatman");
            
            await Task.Delay(-1);
        }

        private Task OnWhisperReceivedAsync()
        {
            throw new NotImplementedException();
        }

        private Task OnLogAsync(LogMessage msg)
        {
            Console.WriteLine(msg);
            return Task.CompletedTask;
        }
    }
}