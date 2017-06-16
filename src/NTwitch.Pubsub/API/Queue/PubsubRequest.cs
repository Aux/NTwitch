using NTwitch.Pubsub.API;
using System.Threading;
using System.Threading.Tasks;

namespace NTwitch.Pubsub.Queue
{
    public class PubsubRequest
    {
        public IWebSocketClient Client { get; }
        public byte[] Data { get; }
        public bool IsText { get; }
        public RequestOptions Options { get; }
        public CancellationToken CancelToken { get; internal set; }

        public PubsubRequest(IWebSocketClient client, string bucketId, byte[] data, bool isText, RequestOptions options)
        {
            Client = client;
            Data = data;
            IsText = isText;
            Options = options;
        }

        public async Task SendAsync()
        {
            await Client.SendAsync(Data, 0, Data.Length, IsText).ConfigureAwait(false);
        }
    }
}
