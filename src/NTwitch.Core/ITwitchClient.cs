using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch
{
    public interface ITwitchClient
    {
        /// <summary>  </summary>
        Task<ISelfChannel> GetCurrentChannelAsync();
        /// <summary>  </summary>
        /// <param name="channelid"></param>
        Task<IChannel> GetChannelAsync(ulong channelid);
        /// <summary>  </summary>
        /// <param name="options"></param>
        Task<IEnumerable<ITopGame>> GetTopGamesAsync(TwitchPageOptions options = null);
        /// <summary>  </summary>
        Task<IEnumerable<IIngest>> GetIngestsAsync();
        /// <summary>  </summary>
        /// <param name="query"></param>
        /// <param name="options"></param>
        Task<IEnumerable<IChannel>> FindChannelsAsync(string query, TwitchPageOptions options = null);
        /// <summary>  </summary>
        /// <param name="query"></param>
        /// <param name="islive"></param>
        Task<IEnumerable<IGame>> FindGamesAsync(string query, bool islive = true);
        /// <summary>  </summary>
        /// <param name="query"></param>
        /// <param name="hls"></param>
        /// <param name="options"></param>
        Task<IEnumerable<IStream>> FindStreamsAsync(string query, bool hls = true, TwitchPageOptions options = null);
        /// <summary>  </summary>
        /// <param name="streamid"></param>
        /// <param name="type"></param>
        Task<IStream> GetStreamAsync(ulong streamid, StreamType type = StreamType.All);
        /// <summary>  </summary>
        /// <param name="game"></param>
        /// <param name="channelids"></param>
        /// <param name="language"></param>
        /// <param name="type"></param>
        /// <param name="options"></param>
        Task<IEnumerable<IStream>> GetStreamsAsync(string game = null, ulong[] channelids = null, string language = null, StreamType type = StreamType.All, TwitchPageOptions options = null);
        /// <summary>  </summary>
        /// <param name="options"></param>
        Task<IEnumerable<IFeaturedStream>> GetFeaturedStreamsAsync(TwitchPageOptions options = null);
        /// <summary>  </summary>
        /// <param name="game"></param>
        Task<IStreamSummary> GetStreamSummaryAsync(string game);
        /// <summary>  </summary>
        /// <param name="options"></param>
        Task<IEnumerable<ITeamSummary>> GetTeamsAsync(TwitchPageOptions options = null);
        /// <summary>  </summary>
        /// <param name="name"></param>
        Task<ITeam> GetTeamAsync(string name);
        /// <summary>  </summary>
        /// <returns></returns>
        Task<ISelfUser> GetCurrentUserAsync();
        /// <summary>  </summary>
        /// <param name="userid"></param>
        Task<IUser> GetUserAsync(ulong userid);
        /// <summary>  </summary>
        /// <param name="name"></param>
        Task<IEnumerable<IUser>> FindUsersAsync(string name);
        /// <summary>  </summary>
        /// <param name="game"></param>
        /// <param name="period"></param>
        /// <param name="type"></param>
        /// <param name="options"></param>
        Task<IEnumerable<IVideo>> GetTopVideosAsync(string game = null, VideoPeriod period = VideoPeriod.Week, BroadcastType type = BroadcastType.Highlight, TwitchPageOptions options = null);
    }
}
