using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class GameBase
    {
        internal BaseRestClient Client { get; }
        [JsonProperty("_id")]
        public ulong Id { get; internal set; }

        internal GameBase(BaseRestClient client)
        {
            Client = client;
        }
    }
}
