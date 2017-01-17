using Newtonsoft.Json;
using System;

namespace NTwitch.Rest
{
    public class RestClip : ClipBase
    {
        [JsonProperty("broadcaster")]
        public RestUserSummary Broadcaster { get; private set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; private set; }
        [JsonProperty("curator")]
        public RestUserSummary Curator { get; private set; }
        [JsonProperty("duration")]
        public double Duration { get; private set; }
        [JsonProperty("embed_html")]
        public string EmbedHtml { get; private set; }
        [JsonProperty("embed_url")]
        public string EmbedUrl { get; private set; }
        [JsonProperty("game")]
        public string Game { get; private set; }
        [JsonProperty("thumbnails")]
        public TwitchImage Thumbnails { get; private set; }
        [JsonProperty("title")]
        public string Title { get; private set; }
        [JsonProperty("url")]
        public string Url { get; private set; }
        [JsonProperty("views")]
        public uint Views { get; private set; }
        [JsonProperty("vod")]
        public RestVideo Vod { get; private set; }

        internal RestClip(BaseRestClient client) : base(client) { }

        internal static RestClip Create(BaseRestClient client, string json)
        {
            var clip = new RestClip(client);
            JsonConvert.PopulateObject(json, clip);
            return clip;
        }
    }
}
