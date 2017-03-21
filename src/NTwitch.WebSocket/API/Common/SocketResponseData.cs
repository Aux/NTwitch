using Newtonsoft.Json;

namespace NTwitch.WebSocket
{
    public class SocketResponseData
    {
        [JsonProperty("topics")]
        public string Topic { get; internal set; }
        [JsonProperty("message")]
        public object Content { get; internal set; }
    }
}
