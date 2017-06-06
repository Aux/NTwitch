using Newtonsoft.Json;

namespace NTwitch
{
    internal class ModifyChannel
    {
        [JsonProperty("channel")]
        public ModifyChannelParams Parameters { get; set; } = new ModifyChannelParams();
    }
}
