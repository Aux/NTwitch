using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class RestChannelSummary : IEntity, IChannelSummary
    {
        public TwitchRestClient Client { get; }
        [JsonProperty("_id")]
        public ulong Id { get; internal set; }
        [JsonProperty("display_name")]
        public string DisplayName { get; internal set; }
        [JsonProperty("name")]
        public string Name { get; internal set; }

        internal RestChannelSummary(TwitchRestClient client)
        {
            Client = client;
        }

        public static RestChannelSummary Create(BaseTwitchClient client, string json)
        {
            var channel = new RestChannelSummary(client as TwitchRestClient);
            JsonConvert.PopulateObject(json, channel);
            return channel;
        }

        ITwitchClient IEntity.Client
            => Client;
    }
}
