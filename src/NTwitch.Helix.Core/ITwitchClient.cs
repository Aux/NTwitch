using System.Threading.Tasks;

namespace NTwitch.Helix
{
    public interface ITwitchClient
    {
        ConnectionState ConnectionState { get; }

        Task StartAsync();
        Task StopAsync();
    }
}
