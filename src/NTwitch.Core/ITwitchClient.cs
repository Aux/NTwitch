using System.Threading.Tasks;

namespace NTwitch
{
    public interface ITwitchClient
    {
        Task LoginAsync(string token);
        Task ConnectAsync();
        Task DisconnectAsync();
    }
}
