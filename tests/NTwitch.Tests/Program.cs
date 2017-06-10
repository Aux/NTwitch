using NTwitch.Chat;
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

                await _client.LoginAsync(token);

                await _client.ListenWhispersAsync(_client.TokenInfo.UserId);
                await _client.ListenVideoPlaybackAsync(_client.TokenInfo.UserId);
                
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
        
        private Task OnLogAsync(LogMessage msg)
        {
            return Console.Out.WriteLineAsync(msg.ToString());
        }
    }
}