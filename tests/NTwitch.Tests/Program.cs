using NTwitch;
using NTwitch.Chat;
using System;
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
        _client.JoinedChannel += OnJoinedChannel;

        await _client.ConnectAsync();
        await _client.LoginAsync("datdoggo", "");
        
        await _client.JoinAsync("wraxu");

        await Task.Delay(-1);
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