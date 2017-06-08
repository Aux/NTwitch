using Newtonsoft.Json;

namespace NTwitch.Rest.API
{
    internal class Emote
    {
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("id")]
        public uint Id { get; set; }
    }
}
