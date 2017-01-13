using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class EmoteBase
    {
        internal BaseRestClient Client { get; }
        [JsonProperty("_id")]
        public ulong Id { get; private set; }

        internal EmoteBase(BaseRestClient client)
        {
            Client = client;
        }
    }
}
