using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("NTwitch.Pubsub")]
namespace NTwitch.Rest
{
    public class RestChannel : IEntity, IChannel
    {
        public TwitchRestClient Client { get; }
        public ulong Id { get; internal set; }
        public string BroadcasterLanguage { get; internal set; }
        public DateTime CreatedAt { get; internal set; }
        public string DisplayName { get; internal set; }
        public int FollowerCount { get; internal set; }
        public string Game { get; internal set; }
        public string Language { get; internal set; }
        public string LogoUrl { get; internal set; }
        public bool IsMature { get; internal set; }
        public string Name { get; internal set; }
        public bool IsPartner { get; internal set; }
        public string ProfileBannerUrl { get; internal set; }
        public string ProfileBackgroundColor { get; internal set; }
        public string Status { get; internal set; }
        public DateTime UpdatedAt { get; internal set; }
        public string Url { get; internal set; }
        public string VideoBannerUrl { get; internal set; }
        public int ViewCount { get; internal set; }

        internal RestChannel(ITwitchClient client)
        {
            Client = client as TwitchRestClient;
        }

        public Task<IEnumerable<RestPost>> GetPostsAsync(int comments = 5, TwitchPageOptions options = null)
        {
            throw new NotImplementedException();
        }

        public Task<RestPost> GetPostAsync(ulong id, int comments = 5)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RestUserFollow>> GetFollowersAsync(SortDirection direction = SortDirection.Descending, TwitchPageOptions options = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RestTeam>> GetTeamsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RestVideo>> GetVideosAsync(string language = null, SortMode sort = SortMode.CreatedAt, BroadcastType type = BroadcastType.Highlight, TwitchPageOptions options = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RestBadge>> GetBadgesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RestEmoteSet>> GetEmoteSetsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RestEmoteSet> GetEmoteSetAsync(ulong setid)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RestEmote>> GetEmotesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RestStream> GetStreamAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RestChannelFollow> FollowAsync(bool notify = false)
        {
            throw new NotImplementedException();
        }

        public Task<RestChannelFollow> UnfollowAsync()
        {
            throw new NotImplementedException();
        }

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
