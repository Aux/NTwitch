using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class RestChannelSummary : RestEntity<ulong>
    {
        [JsonProperty("name")]
        public string Name { get; internal set; }
        [JsonProperty("display_name")]
        public string DisplayName { get; internal set; }

        public RestChannelSummary(BaseRestClient client) : base(client) { }
    }
}
