using Newtonsoft.Json;

namespace NTwitch
{
    public enum SortMode
    {
        [JsonProperty("created_at")]
        CreatedAt,
        [JsonProperty("last_broadcast")]
        LastBroadcast,
        [JsonProperty("login")]
        Login
    }
}
