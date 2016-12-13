using Newtonsoft.Json;

namespace Twitch
{
    public class TwitchValidation
    {
        [JsonProperty("authorization")]
        public TwitchAuthorization Authorization { get; }
        [JsonProperty("user_name")]
        public string Username { get; }
        [JsonProperty("valid")]
        public bool IsValid { get; }
    }
}
