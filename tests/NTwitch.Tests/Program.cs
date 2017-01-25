using NTwitch;
using NTwitch.Chat;
using System;
using System.Reflection;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
        => new Program().StartAsync().GetAwaiter().GetResult();

    private TwitchChatClient _client;

    public async Task StartAsync()
    {
        _client = new TwitchChatClient(new TwitchChatConfig()
        {
            LogLevel = LogLevel.Debug
        });

        _client.Log += OnLog;
        _client.MessageReceived += OnMessageReceived;

        await _client.ConnectAsync();
        await _client.LoginAsync("datdoggo", "");
        await _client.JoinAsync("ster");
        
        await Task.Delay(-1);
    }

    private Task OnMessageReceived(ChatMessage msg)
    {
        Console.WriteLine($"[{msg?.Channel?.Name}] {msg?.User?.DisplayName}: {msg?.Id}");
        return Task.CompletedTask;
    }

    private Task OnLog(LogMessage msg)
    {
        Console.WriteLine($"[{msg.Level}] {msg.Source}: {msg.Message}");
        return Task.CompletedTask;
    }
}