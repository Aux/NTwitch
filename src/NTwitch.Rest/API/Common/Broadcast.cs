using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace NTwitch.Rest.API
{
    internal class Broadcast
    {
        [JsonProperty("_id")]
        public ulong Id { get; set; }
        [JsonProperty("game")]
        public string Game { get; set; }
        [JsonProperty("viewers")]
        public uint ViewerCount { get; set; }
        [JsonProperty("video_height")]
        public uint VideoHeight { get; set; }
        [JsonProperty("average_fps")]
        public double AverageFps { get; set; }
        [JsonProperty("delay")]
        public int Delay { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("is_playlist")]
        public bool IsPlaylist { get; set; }
        [JsonProperty("preview")]
        public IReadOnlyDictionary<string, string> Previews { get; set; }
        [JsonProperty("channel")]
        public Channel Channel { get; set; }

        // ???
        [JsonProperty("channels")]
        public Optional<uint> Channels { get; set; }
    }
}
