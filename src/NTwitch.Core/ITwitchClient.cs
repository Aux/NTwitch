using System.Threading.Tasks;

namespace NTwitch
{
    public interface ITwitchClient
    {
        Task LoginAsync(TokenType type, string token);
        Task ConnectAsync();
        Task DisconnectAsync();
    }
}
