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

        public static RestChannelSummary Create(BaseRestClient client, string json)
        {
            var channel = new RestChannelSummary(client);
            JsonConvert.PopulateObject(json, channel);
            return channel;
        }
    }
}
