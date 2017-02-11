using Newtonsoft.Json;
using System;

namespace NTwitch.Rest
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class RestStream : RestEntity<ulong>
    {
        [JsonProperty("stream.game")]
        public string GameName { get; internal set; }
        [JsonProperty("stream.viewers")]
        public int ViewerTotal { get; internal set; }
        [JsonProperty("stream.video_height")]
        public int VideoHeight { get; internal set; }
        [JsonProperty("stream.average_fps")]
        public double AverageFps { get; internal set; }
        [JsonProperty("stream.delay")]
        public int Delay { get; internal set; }
        [JsonProperty("stream.created_at")]
        public DateTime CreatedAt { get; internal set; }
        [JsonProperty("stream.is_playlist")]
        public bool IsPlaylist { get; internal set; }
        [JsonProperty("stream.preview")]
        public TwitchImage Preview { get; internal set; }
        [JsonProperty("stream.channel")]
        public RestChannel Channel { get; internal set; }

        public RestStream(BaseRestClient client) : base(client) { }

        public static RestStream Create(BaseRestClient client, string json)
        {
            var stream = new RestStream(client);
            JsonConvert.PopulateObject(json, stream);
            return stream;
        }
    }
}
