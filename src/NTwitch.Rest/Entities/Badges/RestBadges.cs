using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class RestBadges
    {
        [JsonProperty("admin")]
        public RestBadgeImage Admin { get; private set; }
        [JsonProperty("broadcaster")]
        public RestBadgeImage Broadcaster { get; private set; }
        [JsonProperty("global_mod")]
        public RestBadgeImage GlobalModerator { get; private set; }
        [JsonProperty("mod")]
        public RestBadgeImage Moderator { get; private set; }
        [JsonProperty("staff")]
        public RestBadgeImage Staff { get; private set; }
        [JsonProperty("subscriber")]
        public RestBadgeImage Subscriber { get; private set; }
        [JsonProperty("turbo")]
        public RestBadgeImage Turbo { get; private set; }

        internal static RestBadges Create(string json)
        {
            var badges = new RestBadges();
            JsonConvert.PopulateObject(json, badges);
            return badges;
        }
    }
}
