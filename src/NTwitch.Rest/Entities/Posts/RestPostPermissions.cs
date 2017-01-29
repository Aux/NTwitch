using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class RestPostPermissions
    {
        [JsonProperty("can_delete")]
        public bool? CanDelete { get; internal set; }
        [JsonProperty("can_moderate")]
        public bool? CanModerate { get; internal set; }
        [JsonProperty("can_reply")]
        public bool? CanReply { get; internal set; }
    }
}
