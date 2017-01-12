using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class TeamBase
    {
        internal BaseRestClient Client { get; }
        [JsonProperty("_id")]
        public ulong Id { get; internal set; }

        internal TeamBase(BaseRestClient client)
        {
            Client = client;
        }
    }
}
