using NTwitch;
using NTwitch.Rest;
using System;
using System.Reflection;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
        => new Program().StartAsync().GetAwaiter().GetResult();

    private TwitchRestClient _client;

    public async Task StartAsync()
    {
        _client = new TwitchRestClient(new TwitchRestConfig()
        {
            LogLevel = LogLevel.Debug
        });

        _client.Log += OnLog;
        
        await _client.LoginAsync(TokenType.OAuth, "");
        var user = _client.Token;

        var properties = user.GetType().GetTypeInfo();
        foreach (var p in properties.GetProperties())
            Console.WriteLine($"{p.Name}: {p.GetValue(user)}");
        
        await Task.Delay(-1);
    }

    private Task OnLog(LogMessage msg)
    {
        Console.WriteLine($"[{msg.Level}] {msg.Source}: {msg.Message}");
        return Task.CompletedTask;
    }
}