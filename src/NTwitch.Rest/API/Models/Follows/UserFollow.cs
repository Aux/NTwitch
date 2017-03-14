using Newtonsoft.Json;

namespace NTwitch.Rest.API
{
    internal class UserFollow : Follow
    {
        [JsonProperty("user")]
        public User User { get; set; }
    }
}
