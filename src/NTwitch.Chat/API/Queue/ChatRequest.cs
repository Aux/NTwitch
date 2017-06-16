using System.Threading.Tasks;

namespace NTwitch.Chat.Queue
{
    public class ChatRequest
    {
        public ISocketClient Client { get; }
        public string Message { get; }
        public RequestOptions Options { get; }

        public ChatRequest(ISocketClient client, string message, RequestOptions options)
        {
            Client = client;
            Message = message;
            Options = options;
        }

        public virtual async Task SendAsync()
        {
            await Client.SendAsync(Message).ConfigureAwait(false);
        }
    }
}
