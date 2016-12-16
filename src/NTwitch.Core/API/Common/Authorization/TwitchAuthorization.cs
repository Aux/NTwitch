using Newtonsoft.Json;
using System;

namespace NTwitch
{
    public class TwitchAuthorization
    {
        [JsonProperty("created_at")]
        DateTime CreatedAt { get; }
        [JsonProperty("updated_at")]
        DateTime UpdatedAt { get; }
        [JsonProperty("scopes")]
        TwitchScope[] Scopes { get; }
    }
}
