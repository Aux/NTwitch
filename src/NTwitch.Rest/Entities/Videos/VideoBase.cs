using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class VideoBase
    {
        internal BaseRestClient Client { get; }
        [JsonProperty("_id")]
        public ulong Id { get; internal set; }

        internal VideoBase(BaseRestClient client)
        {
            Client = client;
        }
    }
}
