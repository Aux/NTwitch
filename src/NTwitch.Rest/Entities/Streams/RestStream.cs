using Newtonsoft.Json;
using System;

namespace NTwitch.Rest
{
    public class RestStream : RestEntity<ulong>
    {
        [TwitchJsonProperty("game")]
        public string GameName { get; internal set; }
        [TwitchJsonProperty("viewers")]
        public int ViewerTotal { get; internal set; }
        [TwitchJsonProperty("video_height")]
        public int VideoHeight { get; internal set; }
        [TwitchJsonProperty("average_fps")]
        public double AverageFps { get; internal set; }
        [TwitchJsonProperty("delay")]
        public int Delay { get; internal set; }
        [TwitchJsonProperty("created_at")]
        public DateTime CreatedAt { get; internal set; }
        [TwitchJsonProperty("is_playlist")]
        public bool IsPlaylist { get; internal set; }
        [TwitchJsonProperty("preview")]
        public TwitchImage Preview { get; internal set; }
        [TwitchJsonProperty("channel")]
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
