using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public class ChannelBase
    {
        internal BaseRestClient Client { get; }
        [JsonProperty("_id"), ChatProperty("room-id")]
        public ulong Id { get; internal set; }
        [JsonProperty("name"), ChatValueBetween("PRIVMSG #", " ")]
        public string Name { get; internal set; }

        internal ChannelBase(BaseRestClient client)
        {
            Client = client;
        }

        public Task<IEnumerable<RestPost>> GetPostsAsync(int comments = 5, PageOptions options = null)
            => ChannelHelper.GetPostsAsync(this, comments, options);

        public Task<RestPost> GetPostAsync(ulong id, int comments = 5)
            => ChannelHelper.GetPostAsync(this, id, comments);

        public Task<IEnumerable<RestUserFollow>> GetFollowersAsync(SortDirection direction = SortDirection.Descending, PageOptions options = null)
            => ChannelHelper.GetFollowersAsync(this, direction, options);

        public Task<IEnumerable<RestTeam>> GetTeamsAsync()
            => ChannelHelper.GetTeamsAsync(this);

        public Task<IEnumerable<RestVideo>> GetVideosAsync(string language = null, SortMode sort = SortMode.CreatedAt, BroadcastType type = BroadcastType.Highlight, PageOptions options = null)
            => ChannelHelper.GetVideosAsync(this, language, sort, type, options);

        public Task<IEnumerable<RestBadges>> GetBadgesAsync()
            => ChannelHelper.GetBadgesAsync(this);

        public Task<IEnumerable<RestEmoteSet>> GetEmoteSetsAsync()
            => ChannelHelper.GetEmoteSetAsync(this);

        public Task<RestEmoteSet> GetEmoteSetAsync(ulong setid)
            => ChannelHelper.GetEmoteSetAsync(this, setid);

        public Task<IEnumerable<RestEmote>> GetEmotesAsync()
            => ChannelHelper.GetEmotesAsync(this);

        public Task<RestStream> GetStreamAsync()
            => ClientHelper.GetStreamAsync(Client, Id, StreamType.All);

        public Task FollowAsync(bool notify = false)
            => ChannelHelper.FollowAsync(this, notify);

        public Task UnfollowAsync()
            => ChannelHelper.UnfollowAsync(this);

        public Task<IEnumerable<RestClip>> GetTopClipsAsync(string game, VideoPeriod period = VideoPeriod.Week, bool istrending = false, PageOptions options = null)
            => ChannelHelper.GetTopClipsAsync(this, game, period, istrending, options);

        public Task<RestClip> GetClipAsync(string id)
            => ChannelHelper.GetClipAsync(this, id);
    }
}
