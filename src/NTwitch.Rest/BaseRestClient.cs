using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public class BaseRestClient : ITwitchClient
    {
        internal RestClient ApiClient => _rest;
        internal LogManager Logger => _log;

        private LogManager _log;
        private RestClient _rest;
        private string _resthost;
        
        public BaseRestClient(TwitchRestConfig config)
        {
            _log = new LogManager(config.LogLevel);
        }

        internal Task LoginInternalAsync(string clientid, string token)
        {
            _rest = new RestClient(_log, _resthost, clientid, token);
            return Task.CompletedTask;
        }

        public Task<RestSelfUser> GetCurrentUserAsync()
            => ClientHelper.GetCurrentUserAsync(this);

        public Task<RestSelfChannel> GetCurrentChannelAsync()
            => ClientHelper.GetCurrentChannelAsync(this);

        public Task<RestChannel> GetChannelAsync(ulong id)
            => ClientHelper.GetChannelAsync(this, id);

        public Task<IEnumerable<RestTopGame>> GetTopGamesAsync(PageOptions options = null)
            => ClientHelper.GetTopGamesAsync(this, options);

        public Task<IEnumerable<RestIngest>> GetIngestsAsync()
            => ClientHelper.GetIngestsAsync(this);

        public Task<IEnumerable<RestChannel>> FindChannelsAsync(string query, PageOptions options = null)
            => ClientHelper.FindChannelsAsync(this, query, options);

        public Task<IEnumerable<RestGame>> FindGamesAsync(string query, bool islive = true)
            => ClientHelper.FindGamesAsync(this, query, islive);

        public Task<RestStream> GetStreamAsync(ulong id, StreamType type = StreamType.All)
            => ClientHelper.GetStreamAsync(this, id, type);

        public Task<IEnumerable<RestStream>> FindStreamsAsync(string query, bool hls = true, PageOptions options = null)
            => ClientHelper.FindStreamsAsync(this, query, hls, options);

        public Task<IEnumerable<RestStream>> GetStreamsAsync(string game = null, ulong[] channelids = null, string language = null, StreamType type = StreamType.All, PageOptions options = null)
            => ClientHelper.GetStreamsAsync(this, game, channelids, language, type, options);

        public Task<IEnumerable<RestFeaturedStream>> GetFeaturedStreamsAsync(PageOptions options = null)
            => ClientHelper.GetFeaturedStreamsAsync(this, options);

        public Task<RestStreamSummary> GetStreamSummaryAsync(string game)
            => ClientHelper.GetStreamSummaryAsync(this, game);

        public Task<IEnumerable<RestTeamSummary>> GetTeamsAsync(PageOptions options = null)
            => ClientHelper.GetTeamsAsync(this, options);

        public Task<RestTeam> GetTeamAsync(string name)
            => ClientHelper.GetTeamAsync(this, name);

        public Task<RestUser> GetUserAsync(ulong id)
            => ClientHelper.GetUserAsync(this, id);

        public Task<IEnumerable<RestUser>> FindUsersAsync(string name)
            => ClientHelper.FindUsersAsync(this, name);

        public Task<IEnumerable<RestVideo>> GetTopVideosAsync(string game = null, VideoPeriod period = VideoPeriod.Week, BroadcastType type = BroadcastType.Highlight, PageOptions options = null)
            => ClientHelper.GetTopVideosAsync(this, game, period, type, options);

        Task ITwitchClient.ConnectAsync()
            => Task.CompletedTask;
        Task ITwitchClient.DisconnectAsync()
            => Task.CompletedTask;
        Task ITwitchClient.LoginAsync()
            => Task.CompletedTask;
    }
}
