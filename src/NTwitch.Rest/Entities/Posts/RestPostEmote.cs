using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class RestPostEmote : RestEntity
    {
        [JsonProperty("start")]
        public int StartIndex { get; internal set; }
        [JsonProperty("end")]
        public int EndIndex { get; internal set; }
        [JsonProperty("set")]
        public uint SetId { get; internal set; }

        public RestPostEmote(BaseRestClient client) : base(client) { }
    }
}
