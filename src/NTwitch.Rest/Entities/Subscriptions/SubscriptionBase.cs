using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class SubscriptionBase
    {
        internal BaseRestClient Client { get; }
        [JsonProperty("_id"), ChatProperty("")]
        public ulong Id { get; internal set; }

        public SubscriptionBase(BaseRestClient client)
        {
            Client = client;
        }
    }
}
