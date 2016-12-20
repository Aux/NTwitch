using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public class RestChannel : IChannel
    {
        [JsonIgnore]
        public TwitchRestClient Client { get; }
        [JsonProperty("background")]
        public string Background { get; }
        [JsonProperty("banner")]
        public string BannerUrl { get; }
        [JsonProperty("broadcaster_language")]
        public string BroadcasterLanguage { get; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; }
        [JsonProperty("delay")]
        public int Delay { get; }
        [JsonProperty("display_name")]
        public string DisplayName { get; }
        [JsonProperty("followers")]
        public int Followers { get; }
        [JsonProperty("game")]
        public string Game { get; }
        [JsonProperty("_id")]
        public uint Id { get; }
        [JsonProperty("mature")]
        public bool IsMature { get; }
        [JsonProperty("partner")]
        public bool IsPartner { get; }
        [JsonProperty("language")]
        public string Language { get; }
        [JsonProperty("_links")]
        public TwitchLinks Links { get; }
        [JsonProperty("logo")]
        public string LogoUrl { get; }
        [JsonProperty("name")]
        public string Name { get; }
        [JsonProperty("profile_banner_background_color")]
        public string ProfileBackground { get; }
        [JsonProperty("profile_banner")]
        public string ProfileBannerUrl { get; }
        [JsonProperty("status")]
        public string Status { get; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; }
        [JsonProperty("url")]
        public string Url { get; }
        [JsonProperty("video_banner")]
        public string VideoBannerUrl { get; }
        [JsonProperty("views")]
        public int Views { get; }

        public Task<IEnumerable<IBadge>> GetBadgesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IUserFollow> GetFollowAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IUserFollow>> GetFollowersAsync(SortDirection sort = SortDirection.Descending, int limit = 10, int page = 1)
        {
            throw new NotImplementedException();
        }

        public Task<IUserSubscription> GetSubscriberAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IUserSubscription>> GetSubscribersAsync(SortDirection sort = SortDirection.Descending, int limit = 10, int page = 1)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IVideo>> GetVideosAsync(bool broadcasts = false, bool hls = false, int limit = 10, int page = 1)
        {
            throw new NotImplementedException();
        }
    }
}
