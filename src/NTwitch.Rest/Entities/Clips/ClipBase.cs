using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class ClipBase
    {
        internal BaseRestClient Client { get; }
        [JsonProperty("id")]
        public string Id { get; private set; }

        internal ClipBase(BaseRestClient client)
        {
            Client = client;
        }
    }
}
