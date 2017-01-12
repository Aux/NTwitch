using Newtonsoft.Json;
using System;

namespace NTwitch.Rest
{
    public class RestChannel : ChannelBase
    {
        [JsonProperty("broadcaster_language")]
        public string BroadcasterLanguage { get; internal set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; internal set; }
        [JsonProperty("display_name")]
        public string DisplayName { get; internal set; }
        [JsonProperty("followers")]
        public int FollowerCount { get; internal set; }
        [JsonProperty("game")]
        public string Game { get; internal set; }
        [JsonProperty("language")]
        public string Language { get; internal set; }
        [JsonProperty("logo")]
        public string LogoUrl { get; internal set; }
        [JsonProperty("mature")]
        public bool IsMature { get; internal set; }
        [JsonProperty("name")]
        public string Name { get; internal set; }
        [JsonProperty("partner")]
        public bool IsPartner { get; internal set; }
        [JsonProperty("profile_banner")]
        public string ProfileBannerUrl { get; internal set; }
        [JsonProperty("profile_banner_background_color")]
        public string ProfileBackgroundColor { get; internal set; }
        [JsonProperty("Status")]
        public string Status { get; internal set; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; internal set; }
        [JsonProperty("url")]
        public string Url { get; internal set; }
        [JsonProperty("video_banner")]
        public string VideoBannerUrl { get; internal set; }
        [JsonProperty("views")]
        public int ViewCount { get; internal set; }

        public RestChannel(TwitchRestClient client) : base(client) { }
        
        public static RestChannel Create(BaseRestClient client, string json)
        {
            var channel = new RestChannel(client as TwitchRestClient);
            JsonConvert.PopulateObject(json, channel);
            return channel;
        }
    }
}
