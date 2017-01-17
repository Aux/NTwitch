using System.Threading.Tasks;

namespace NTwitch
{
    public interface ITwitchClient
    {
        Task ConnectAsync();
        Task LoginAsync();
        Task DisconnectAsync();
    }
}
