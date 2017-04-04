using System.Threading.Tasks;

namespace NTwitch
{
    public interface ISocketClient
    {
        Task SendAsync(string message);
        Task ConnectAsync();
        Task DisconnectAsync(bool disposing = false);
    }
}
