using NTwitch;
using NTwitch.Chat;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
        => new Program().Start().GetAwaiter().GetResult();

    private TwitchChatClient _client;

    public async Task Start()
    {
        _client = new TwitchChatClient(new TwitchChatConfig()
        {
            LogLevel = LogLevel.Info
        });

        _client.MessageReceived += OnMessageReceived;

        await _client.ConnectAsync();
        await _client.LoginAsync("datdoggo", "");
    }
}