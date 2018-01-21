using System;
using System.Threading.Tasks;

namespace NTwitch
{
    public interface ITwitchClient : IDisposable
    {
        ConnectionState ConnectionState { get; }

        Task StartAsync();
        Task StopAsync();
    }
}
