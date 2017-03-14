using Newtonsoft.Json;

namespace NTwitch.Rest.API
{
    internal class ChannelFollow : Follow
    {
        [JsonProperty("channel")]
        public Channel Channel { get; set; }
    }
}
