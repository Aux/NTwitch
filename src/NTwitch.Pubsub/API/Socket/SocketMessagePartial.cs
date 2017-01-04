using Newtonsoft.Json;

namespace NTwitch.Pubsub
{
    public class SocketMessagePartial
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        public SocketMessagePartial(string type = null)
        {
            Type = type;
        }

        public override string ToString()
            => JsonConvert.SerializeObject(this);
    }
}
