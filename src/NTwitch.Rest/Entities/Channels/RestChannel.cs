using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public class RestChannel : IEntity, IChannel
    {
        public TwitchRestClient Client { get; }
        [JsonProperty("_id")]
        public ulong Id { get; internal set; }
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

        internal RestChannel(TwitchRestClient client)
        {
            Client = client;
        }

        internal static RestChannel Create(BaseTwitchClient client, string json)
        {
            var channel = new RestChannel(client as TwitchRestClient);
            JsonConvert.PopulateObject(json, channel);
            return channel;
        }

        public async Task<IEnumerable<RestPost>> GetPostsAsync(int comments = 5, TwitchPageOptions options = null)
            => await ChannelHelper.GetPostsAsync(this, comments, options);

        public async Task<RestPost> GetPostAsync(ulong id, int comments = 5)
            => await ChannelHelper.GetPostAsync(this, id, comments);

        public async Task<IEnumerable<RestUserFollow>> GetFollowersAsync(SortDirection direction = SortDirection.Descending, TwitchPageOptions options = null)
            => await ChannelHelper.GetFollowersAsync(this, direction, options);

        public async Task<IEnumerable<RestTeam>> GetTeamsAsync()
            => await ChannelHelper.GetTeamsAsync(this);

        public async Task<IEnumerable<RestVideo>> GetVideosAsync(string language = null, SortMode sort = SortMode.CreatedAt, BroadcastType type = BroadcastType.Highlight, TwitchPageOptions options = null)
            => await ChannelHelper.GetVideosAsync(this, language, sort, type, options);

        public async Task<IEnumerable<RestBadge>> GetBadgesAsync()
            => await ChannelHelper.GetBadgesAsync(this);

        public async Task<IEnumerable<RestEmoteSet>> GetEmoteSetsAsync()
            => await ChannelHelper.GetEmoteSetAsync(this);

        public async Task<RestEmoteSet> GetEmoteSetAsync(ulong setid)
            => await ChannelHelper.GetEmoteSetAsync(this, setid);

        public async Task<IEnumerable<RestEmote>> GetEmotesAsync()
            => await ChannelHelper.GetEmotesAsync(this);

        public async Task<RestStream> GetStreamAsync()
            => await ClientHelper.GetStreamAsync(Client, Id, StreamType.All);

        public async Task<RestChannelFollow> FollowAsync(bool notify = false)
            => await ChannelHelper.FollowAsync(this, notify);

        public async Task<RestChannelFollow> UnfollowAsync()
            => await ChannelHelper.UnfollowAsync(this);

        //IChannel
        ITwitchClient IEntity.Client
            => Client;
        async Task<IEnumerable<IPost>> IChannel.GetPostsAsync(int comments, TwitchPageOptions options)
            => await GetPostsAsync(comments, options);
        async Task<IPost> IChannel.GetPostAsync(ulong id, int comments)
            => await GetPostAsync(id, comments);
        async Task<IEnumerable<IUserFollow>> IChannel.GetFollowersAsync(SortDirection direction, TwitchPageOptions options)
            => await GetFollowersAsync(direction, options);
        async Task<IEnumerable<ITeam>> IChannel.GetTeamsAsync()
            => await GetTeamsAsync();
        async Task<IEnumerable<IVideo>> IChannel.GetVideosAsync(string language, SortMode sort, BroadcastType type, TwitchPageOptions options)
            => await GetVideosAsync(language, sort, type, options);
        async Task<IEnumerable<IBadge>> IChannel.GetBadgesAsync()
            => await GetBadgesAsync();
        async Task<IEnumerable<IEmoteSet>> IChannel.GetEmoteSetsAsync()
            => await GetEmoteSetsAsync();
        async Task<IEmoteSet> IChannel.GetEmoteSetAsync(ulong setid)
            => await GetEmoteSetAsync(setid);
        async Task<IEnumerable<IEmote>> IChannel.GetEmotesAsync()
            => await GetEmotesAsync();
        async Task<IStream> IChannel.GetStreamAsync()
            => await GetStreamAsync();
        async Task<IChannelFollow> IChannel.FollowAsync(bool notify)
            => await FollowAsync(notify);
        async Task<IChannelFollow> IChannel.UnfollowAsync()
            => await UnfollowAsync();
    }
}
