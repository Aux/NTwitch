using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class RestChannelSummary : ChannelBase
    {
        [JsonProperty("display_name")]
        public string DisplayName { get; private set; }

        public RestChannelSummary(TwitchRestClient client) : base(client) { }

        public static RestChannelSummary Create(BaseRestClient client, string json)
        {
            var channel = new RestChannelSummary(client as TwitchRestClient);
            JsonConvert.PopulateObject(json, channel);
            return channel;
        }
    }
}
