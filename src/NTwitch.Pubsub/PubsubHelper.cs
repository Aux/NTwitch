using Newtonsoft.Json;
using NTwitch.Pubsub.API;
using System.Threading.Tasks;

namespace NTwitch.Pubsub
{
    internal static class PubsubHelper
    {
        public static async Task HandleWhisperAsync(BasePubsubClient client, string content)
        {
            var model = JsonConvert.DeserializeObject<WhisperMessage>(content);
            var entity = PubsubWhisperMessage.Create(client, model);
            await client.whisperReceivedEvent.InvokeAsync(entity).ConfigureAwait(false);
        }
    }
}
