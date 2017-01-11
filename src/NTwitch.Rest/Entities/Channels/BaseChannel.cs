using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public class BaseChannel
    {
        internal BaseRestClient Client { get; }
        [JsonProperty("_id")]
        public ulong Id { get; internal set; }

        public BaseChannel(BaseRestClient client)
        {
            Client = client;
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
    }
}
