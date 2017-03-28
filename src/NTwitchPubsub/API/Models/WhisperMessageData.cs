using Newtonsoft.Json;

namespace NTwitch.Pubsub.API
{
    internal class WhisperMessageData
    {
        [JsonProperty("id")]
        public ulong Id { get; set; }
    }
}
