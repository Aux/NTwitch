using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class StreamBase
    {
        internal BaseRestClient Client { get; }
        [JsonProperty("_id")]
        public ulong Id { get; internal set; }

        internal StreamBase(BaseRestClient client)
        {
            Client = client;
        }
    }
}
