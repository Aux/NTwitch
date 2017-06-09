using System.Threading.Tasks;

namespace NTwitch.Pubsub
{
    public interface IWebSocketClient : ISocketClient
    {
        void SetHeader(string key, string value);

        Task SendAsync(byte[] data, int index, int count, bool isText);
    }
}
