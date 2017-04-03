using Newtonsoft.Json;

namespace NTwitch.Rest.API
{
    internal class Token
    {
        [JsonProperty("valid")]
        public bool IsValid { get; set; } = false;
        [JsonProperty("user_name")]
        public string Username { get; set; }
        [JsonProperty("user_id")]
        public ulong UserId { get; set; }
        [JsonProperty("client_id")]
        public string ClientId { get; set; }
        [JsonProperty("authorization")]
        public Authorization Authorization { get; set; }
    }
}
