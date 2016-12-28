using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch
{
    public interface ITwitchClient
    {
        ConnectionState ConnectionState { get; }

        Task LoginAsync();
        Task<IEnumerable<ITopGame>> GetTopGames(TwitchPageOptions options = null);
        Task<IEnumerable<IIngest>> GetIngestsAsync();
        Task<IEnumerable<IChannel>> FindChannelsAsync(string query, TwitchPageOptions options = null);
        Task<IEnumerable<IGame>> FindGamesAsync(string query, bool islive = true);
        Task<IStream> GetStreamAsync(ulong id, StreamType type = StreamType.All);
        Task<IEnumerable<IStream>> FindStreamsAsync(string query, bool hls = true, TwitchPageOptions options = null);
        Task<IEnumerable<IStream>> GetStreamsAsync(string game = null, ulong[] channelids = null, string language = null, StreamType type = StreamType.All, TwitchPageOptions options = null);
        Task<IEnumerable<IFeaturedStream>> GetFeaturedStreamsAsync(TwitchPageOptions options = null);
        Task<IEnumerable<IStreamSummary>> GetStreamSummaryAsync(string game);
        Task<IEnumerable<ITeamInfo>> GetTeamsAsync(TwitchPageOptions options = null);
        Task<IEnumerable<ITeam>> GetTeamAsync(string name);
    }
}
