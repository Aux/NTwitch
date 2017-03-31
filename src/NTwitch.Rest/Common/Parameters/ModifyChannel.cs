using Newtonsoft.Json;

namespace NTwitch.Rest
{
    internal class ModifyChannel
    {
        [JsonProperty("channel")]
        public ModifyChannelParams Parameters { get; set; } = new ModifyChannelParams();
    }
}
