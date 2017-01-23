using Newtonsoft.Json;

namespace NTwitch.Rest
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class TokenInfo
    {
        [JsonProperty("token.valid")]
        public bool IsValid { get; internal set; }
        [JsonProperty("token.user_id")]
        public ulong UserId { get; internal set; }
        [JsonProperty("token.user_name")]
        public string Username { get; internal set; }
        [JsonProperty("token.client_id")]
        public string ClientId { get; internal set; }
        [JsonProperty("token.authorization")]
        public TwitchAuth Authorization { get; internal set; }
        
        internal static TokenInfo Create(string json)
        {
            var token = JsonConvert.DeserializeObject<TokenInfo>(json);
            return token;
        }
    }
}
