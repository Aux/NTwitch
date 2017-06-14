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
            string token = "";
            string clientId = "";

            try
            {
                _client = new TwitchPubsubClient(new TwitchPubsubConfig
                {
                    ClientId = clientId,
                    LogLevel = LogSeverity.Debug
                });

                _client.Log += OnLogAsync;
                _client.AnonymousReceived += AnonymousReceivedAsync;

                await _client.LoginAsync(token);

                var id = _client.TokenInfo.UserId;
                await _client.ListenWhispersAsync(id);

                await Task.Delay(5000);
                await _client.ListenAsync($"video-playback.{id}", $"video-playback.{_client.TokenInfo.Username}");

                //_client = new TwitchChatClient(new TwitchChatConfig
                //{
                //    ClientId = clientId,
                //    LogLevel = LogSeverity.Debug,
                //    SocketClientProvider = DefaultWebSocketProvider.Instance,
                //    SocketHost = "wss://irc-ws.chat.twitch.tv"
                //});

                //_client.Log += OnLogAsync;

                //await _client.LoginAsync(token);
                //await _client.ConnectAsync();

                //await _client.JoinChannelAsync("wraxu");
                //await _client.JoinChannelAsync("timthetatman");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            await Task.Delay(-1);
        }

        private Task AnonymousReceivedAsync(string arg)
        {
            return Console.Out.WriteLineAsync(arg);
        }

        private Task OnLogAsync(LogMessage msg)
        {
            return Console.Out.WriteLineAsync(msg.ToString());
        }
    }
}