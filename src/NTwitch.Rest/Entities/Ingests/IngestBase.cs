using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class IngestBase
    {
        internal BaseRestClient Client { get; }
        [JsonProperty("_id")]
        public ulong Id { get; internal set; }

        internal IngestBase(BaseRestClient client)
        {
            Client = client;
        }
    }
}
