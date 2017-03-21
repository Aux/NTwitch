using Newtonsoft.Json;

namespace NTwitch.WebSocket
{
    public class SocketResponse
    {
        [JsonProperty("type")]
        public string Type { get; internal set; }
        [JsonProperty("data")]
        public SocketResponseData Data { get; internal set; }
    }
}
