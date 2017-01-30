using Newtonsoft.Json;

namespace NTwitch
{
    public enum BroadcastType
    {
        [JsonProperty("all")]
        All,
        [JsonProperty("archive")]
        Archive,
        [JsonProperty("highlight")]
        Highlight
    }
}
