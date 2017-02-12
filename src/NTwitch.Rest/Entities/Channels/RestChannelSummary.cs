using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class RestChannelSummary : RestEntity<ulong>
    {
        [TwitchJsonProperty("name")]
        public string Name { get; internal set; }
        [TwitchJsonProperty("display_name")]
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
