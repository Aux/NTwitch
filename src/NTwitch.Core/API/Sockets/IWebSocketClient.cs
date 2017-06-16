using System.Threading.Tasks;

namespace NTwitch.Pubsub
{
    public interface IWebSocketClient : ISocketClient
    {
        Task SendAsync(byte[] data, int index, int count, bool isText);
    }
}
