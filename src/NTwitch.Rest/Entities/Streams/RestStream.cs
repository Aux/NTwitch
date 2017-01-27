using Newtonsoft.Json;
using System;

namespace NTwitch.Rest
{
    public class RestStream : RestEntity
    {
        [JsonProperty("")]
        public double AverageFps { get; private set; }
        [JsonProperty("")]
        public RestChannel Channel { get; private set; }
        [JsonProperty("")]
        public DateTime CreatedAt { get; private set; }
        [JsonProperty("")]
        public int Delay { get; private set; }
        [JsonProperty("")]
        public string Game { get; private set; }
        [JsonProperty("")]
        public bool IsPlaylist { get; private set; }
        [JsonProperty("")]
        public TwitchImage Preview { get; private set; }
        [JsonProperty("")]
        public int VideoHeight { get; private set; }
        [JsonProperty("")]
        public int Viewers { get; private set; }

        public RestStream(BaseRestClient client) : base(client) { }

        public static RestStream Create(BaseRestClient client, string json)
        {
            var stream = new RestStream(client);
            JsonConvert.PopulateObject(json, stream);
            return stream;
        }
    }
}
