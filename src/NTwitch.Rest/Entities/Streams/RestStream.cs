using Newtonsoft.Json;
using System;

namespace NTwitch.Rest
{
    public class RestStream : RestEntity<ulong>
    {
        [JsonProperty("game")]
        public string GameName { get; internal set; }
        [JsonProperty("viewers")]
        public int ViewerTotal { get; internal set; }
        [JsonProperty("video_height")]
        public int VideoHeight { get; internal set; }
        [JsonProperty("average_fps")]
        public double AverageFps { get; internal set; }
        [JsonProperty("delay")]
        public int Delay { get; internal set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; internal set; }
        [JsonProperty("is_playlist")]
        public bool IsPlaylist { get; internal set; }
        [JsonProperty("preview")]
        public TwitchImage Preview { get; internal set; }
        [JsonProperty("channel")]
        public RestChannel Channel { get; internal set; }

        public RestStream(BaseRestClient client) : base(client) { }
    }
}
