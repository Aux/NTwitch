using NTwitch.Rest;
using System;
using System.Threading.Tasks;

namespace NTwitch.Tests
{
    class Program
    {
        public static void Main(string[] args)
            => new Program().Start().GetAwaiter().GetResult();

        private TwitchRestClient _client;

        public async Task Start()
        {
            try
            {
                _client = new TwitchRestClient(new TwitchRestConfig()
                {
                    ClientId = "",
                    LogLevel = LogSeverity.Debug
                });

                _client.Log += OnLogAsync;

                await _client.LoginAsync("");
                var user = await _client.GetCurrentUserAsync();

                Console.WriteLine($"Current Token: {_client.Token?.Username}");
                Console.WriteLine($"Current User: {user?.DisplayName}");

            } catch (Exception ex)
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