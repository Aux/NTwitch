using Newtonsoft.Json;

namespace NTwitch.Pubsub.API
{
    internal class BadgeEntitlement
    {
        [JsonProperty("new_version")]
        public string NewVersion { get; set; }
        [JsonProperty("previous_version")]
        public string OldVersion { get; set; }
    }
}
