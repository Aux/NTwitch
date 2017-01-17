using NTwitch;
using NTwitch.Chat;
using System;
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
        _client.JoinedChannel += OnJoinedChannel;

        await _client.ConnectAsync();
        await _client.LoginAsync("datdoggo", "");

        await Task.Delay(1000);
        await _client.JoinAsync("timthetatman");

        await Task.Delay(-1);
    }

    private Task OnLog(LogMessage msg)
    {
        Console.WriteLine($"[{msg.Level}] {msg.Source}: {msg.Message}");
        return Task.CompletedTask;
    }

    private Task OnMessageReceived(ChatMessage msg)
    {
        Console.WriteLine($"#{msg.Channel.Name} {msg.User.DisplayName}: {msg.Content}");
        return Task.CompletedTask;
    }

    private Task OnJoinedChannel(ChatChannel channel)
    {
        Console.WriteLine($"Joined #{channel.Name} :)");
        return Task.CompletedTask;
    }

}