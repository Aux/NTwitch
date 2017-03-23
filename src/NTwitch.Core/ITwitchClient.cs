using System.Threading.Tasks;

namespace NTwitch
{
    public interface ITwitchClient
    {
        Task LoginAsync(AuthMode type, string token);
        Task ConnectAsync();
        Task DisconnectAsync();
    }
}
