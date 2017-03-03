using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public class RestChannel : RestChannelSummary, IChannel
    {
        [JsonProperty("broadcaster_language")]
        public string BroadcasterLanguage { get; internal set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; internal set; }
        [JsonProperty("followers")]
        public int FollowerCount { get; internal set; }
        [JsonProperty("game")]
        public string Game { get; internal set; }
        [JsonProperty("language")]
        public string Language { get; internal set; }
        [JsonProperty("logo")]
        public string LogoUrl { get; internal set; }
        [JsonProperty("mature")]
        public bool? IsMature { get; internal set; }
        [JsonProperty("partner")]
        public bool IsPartner { get; internal set; }
        [JsonProperty("profile_banner")]
        public string ProfileBannerUrl { get; internal set; }
        [JsonProperty("profile_banner_background_color")]
        public string ProfileBackgroundColor { get; internal set; }
        [JsonProperty("status")]
        public string Status { get; internal set; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; internal set; }
        [JsonProperty("url")]
        public string Url { get; internal set; }
        [JsonProperty("video_banner")]
        public string VideoBannerUrl { get; internal set; }
        [JsonProperty("views")]
        public int ViewCount { get; internal set; }
        
        public RestChannel(BaseRestClient client) : base(client) { }
        
        // Teams
        public Task<IEnumerable<RestTeam>> GetTeamsAsync()
            => ChannelHelper.GetTeamsAsync(this, Client);

        // Posts
        public Task<IEnumerable<RestPost>> GetPostsAsync()
            => GetPostsAsync(5);
        public Task<IEnumerable<RestPost>> GetPostsAsync(int comments = 5, PageOptions options = null)
            => ChannelHelper.GetPostsAsync(this, Client, comments, options);
        public Task<RestPost> GetPostAsync(ulong id)
            => GetPostAsync(id, 5);
        public Task<RestPost> GetPostAsync(ulong id, int comments = 5)
            => ChannelHelper.GetPostAsync(this, Client, id, comments);
        
        // Users
        public Task<IEnumerable<RestUserFollow>> GetFollowersAsync()
            => GetFollowersAsync(false);
        public Task<IEnumerable<RestUserFollow>> GetFollowersAsync(bool ascending = false, PageOptions options = null)
            => ChannelHelper.GetFollowersAsync(this, Client, ascending, options);
        public Task FollowAsync()
            => FollowAsync(false);
        public Task FollowAsync(bool notify = false)
            => ChannelHelper.FollowAsync(this, Client, notify);
        public Task UnfollowAsync()
            => ChannelHelper.UnfollowAsync(this, Client);

        // Videos
        public Task<RestStream> GetStreamAsync()
            => ClientHelper.GetStreamAsync(Client, Id, StreamType.All);
        public Task<IEnumerable<RestVideo>> GetVideosAsync()
            => GetVideosAsync(null);
        public Task<IEnumerable<RestVideo>> GetVideosAsync(string language = null, SortMode sort = SortMode.CreatedAt, BroadcastType type = BroadcastType.Highlight, PageOptions options = null)
            => ChannelHelper.GetVideosAsync(this, Client, language, sort, type, options);
        public Task<IEnumerable<RestClip>> GetTopClipsAsync(string game)
            => GetTopClipsAsync(game, VideoPeriod.Week);
        public Task<IEnumerable<RestClip>> GetTopClipsAsync(string game, VideoPeriod period = VideoPeriod.Week, bool istrending = false, PageOptions options = null)
            => ChannelHelper.GetTopClipsAsync(this, Client, game, period, istrending, options);
        public Task<RestClip> GetClipAsync(string id)
            => ChannelHelper.GetClipAsync(this, Client, id);
        
        // IChannel
        Task IChannel.FollowAsync(bool notify)
            => FollowAsync(notify);
        Task IChannel.UnfollowAsync()
            => UnfollowAsync();
        Task IChannel.GetPostsAsync()
            => GetPostsAsync();
        Task IChannel.GetPostAsync()
            => GetPostsAsync();
        Task IChannel.GetFollowersAsync()
            => GetFollowersAsync();
        Task IChannel.GetTeamsAsync()
            => GetTeamsAsync();
        Task IChannel.GetVideosAsync()
            => GetVideosAsync();
        Task IChannel.GetStreamAsync()
            => GetStreamAsync();
        Task IChannel.GetTopClipsAsync(string game, VideoPeriod period, bool istrending, PageOptions options)
            => GetTopClipsAsync(game, period, istrending, options);
        Task IChannel.GetClipAsync(string clipId)
            => GetClipAsync(clipId);
    }
}
