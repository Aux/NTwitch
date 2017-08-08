using System;
using System.Threading.Tasks;

namespace NTwitch
{
    public interface ITwitchClient : IDisposable
    {
        ConnectionState ConnectionState { get; }
        ISelfUser CurrentUser { get; }

        Task StartAsync();
        Task StopAsync();
    }
}
