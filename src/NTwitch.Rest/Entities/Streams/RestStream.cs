using Newtonsoft.Json;
using System;

namespace NTwitch.Rest
{
    public class RestStream : RestEntity
    {
        [JsonProperty("game")]
        public string GameName { get; private set; }
        [JsonProperty("viewers")]
        public int ViewerTotal { get; private set; }
        [JsonProperty("video_height")]
        public int VideoHeight { get; private set; }
        [JsonProperty("average_fps")]
        public double AverageFps { get; private set; }
        [JsonProperty("delay")]
        public int Delay { get; private set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; private set; }
        [JsonProperty("is_playlist")]
        public bool IsPlaylist { get; private set; }
        [JsonProperty("preview")]
        public TwitchImage Preview { get; private set; }
        [JsonProperty("channel")]
        public RestChannel Channel { get; private set; }

        public RestStream(BaseRestClient client) : base(client) { }

        public static RestStream Create(BaseRestClient client, string json)
        {
            var stream = new RestStream(client);
            JsonConvert.PopulateObject(json, stream);
            return stream;
        }
    }
}
