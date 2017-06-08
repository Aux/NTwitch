using Newtonsoft.Json;

namespace NTwitch.Rest.API
{
    internal class CommunityPermissions
    {
        [JsonProperty("ban")]
        public bool CanBan { get; set; }
        [JsonProperty("timeout")]
        public bool CanTimeout { get; set; }
        [JsonProperty("edit")]
        public bool CanEdit { get; set; }
    }
}
