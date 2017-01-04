using Newtonsoft.Json;

namespace NTwitch.Pubsub
{
    internal class SocketMessage : SocketMessagePartial
    {
        [JsonProperty("data")]
        public SocketData Data { get; set; }
        [JsonProperty("topic")]
        public string Topic { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }

        public SocketMessage() : base() { }
    }
}
