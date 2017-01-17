using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class FollowBase
    {
        internal BaseRestClient Client { get; }
        [JsonProperty("_id"), ChatProperty("")]
        public ulong Id { get; internal set; }

        public FollowBase(BaseRestClient client)
        {
            Client = client;
        }
    }
}
