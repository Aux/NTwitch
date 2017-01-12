using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class RestStreamSummary : StreamBase
    {
        [JsonProperty("channels")]
        public int Channels { get; private set; }
        [JsonProperty("viewers")]
        public int Viewers { get; private set; }

        public RestStreamSummary(TwitchRestClient client) : base(client) { }

        public static RestStreamSummary Create(BaseRestClient client, string json)
        {
            var stream = new RestStreamSummary(client as TwitchRestClient);
            JsonConvert.PopulateObject(json, stream);
            return stream;
        }
    }
}
