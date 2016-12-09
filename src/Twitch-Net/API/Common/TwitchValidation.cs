using Newtonsoft.Json;

namespace Twitch
{
    public class TwitchValidation
    {
        [JsonProperty("authorization")]
        TwitchAuthorization Authorization { get; }
        [JsonProperty("user_name")]
        string Username { get; }
        [JsonProperty("valid")]
        bool IsValid { get; }
    }
}
