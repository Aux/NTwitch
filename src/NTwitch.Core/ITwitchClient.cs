using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch
{
    public interface ITwitchClient
    {
        ConnectionState ConnectionState { get; }

        Task<ISelfChannel> GetCurrentChannelAsync();
        Task<IChannel> GetChannelAsync(ulong channelid);
        Task<IEnumerable<ITopGame>> GetTopGamesAsync(TwitchPageOptions options = null);
        Task<IEnumerable<IIngest>> GetIngestsAsync();
        Task<IEnumerable<IChannel>> FindChannelsAsync(string query, TwitchPageOptions options = null);
        Task<IEnumerable<IGame>> FindGamesAsync(string query, bool islive = true);
        Task<IEnumerable<IStream>> FindStreamsAsync(string query, bool hls = true, TwitchPageOptions options = null);
        Task<IStream> GetStreamAsync(ulong streamid, StreamType type = StreamType.All);
        Task<IEnumerable<IStream>> GetStreamsAsync(string game = null, ulong[] channelids = null, string language = null, StreamType type = StreamType.All, TwitchPageOptions options = null);
        Task<IEnumerable<IFeaturedStream>> GetFeaturedStreamsAsync(TwitchPageOptions options = null);
        Task<IEnumerable<IStreamSummary>> GetStreamSummaryAsync(string game);
        Task<IEnumerable<ITeamInfo>> GetTeamsAsync(TwitchPageOptions options = null);
        Task<IEnumerable<ITeam>> GetTeamAsync(string name);
        Task<ISelfUser> GetCurrentUserAsync();
        Task<IUser> GetUserAsync(ulong userid);
        Task<IEnumerable<IVideo>> GetTopVideosAsync(string game = null, VideoPeriod period = VideoPeriod.Week, BroadcastType type = BroadcastType.Highlight, TwitchPageOptions options = null);
    }
}
