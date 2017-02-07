using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public class BaseRestClient : ITwitchClient
    {
        public TokenInfo Token => _tokeninfo;

        internal RestClient ApiClient => _rest;
        internal LogManager Logger => _log;

        private LogManager _log;
        private RestClient _rest;
        private TokenInfo _tokeninfo;
        private string _resthost;

        internal readonly AsyncEvent<Func<LogMessage, Task>> _logEvent = new AsyncEvent<Func<LogMessage, Task>>();
        public event Func<LogMessage, Task> Log
        {
            add { _logEvent.Add(value); }
            remove { _logEvent.Remove(value); }
        }

        public BaseRestClient(TwitchRestConfig config)
        {
            _resthost = config.RestUrl;

            _log = new LogManager(config.LogLevel);
            _log.LogReceived += OnLogReceived;
        }

        private Task OnLogReceived(LogMessage msg)
            => _logEvent.InvokeAsync(msg);
        
        internal async Task LoginInternalAsync(TokenType type, string token)
        {
            _rest = new RestClient(_log, _resthost);
            _tokeninfo = await _rest.LoginAsync(type, token);
        }

        public Task<RestSelfUser> GetCurrentUserAsync()
            => ClientHelper.GetCurrentUserAsync(this);
        public Task<RestSelfChannel> GetCurrentChannelAsync()
            => ClientHelper.GetCurrentChannelAsync(this);
        public Task<RestChannel> GetChannelAsync(uint id)
            => ClientHelper.GetChannelAsync(this, id);
        public Task<IEnumerable<RestTopGame>> GetTopGamesAsync()
            => GetTopGamesAsync(null);
        public Task<IEnumerable<RestTopGame>> GetTopGamesAsync(PageOptions options = null)
            => ClientHelper.GetTopGamesAsync(this, options);
        public Task<IEnumerable<RestIngest>> GetIngestsAsync()
            => ClientHelper.GetIngestsAsync(this);
        public Task<IEnumerable<RestChannel>> FindChannelsAsync(string query)
            => FindChannelsAsync(query, null);
        public Task<IEnumerable<RestChannel>> FindChannelsAsync(string query, PageOptions options = null)
            => ClientHelper.FindChannelsAsync(this, query, options);
        public Task<IEnumerable<RestGame>> FindGamesAsync(string query)
            => FindGamesAsync(query, true);
        public Task<IEnumerable<RestGame>> FindGamesAsync(string query, bool? islive = null)
            => ClientHelper.FindGamesAsync(this, query, islive);
        public Task<RestStream> GetStreamAsync(uint id)
            => GetStreamAsync(id, StreamType.All);
        public Task<RestStream> GetStreamAsync(uint id, StreamType type = StreamType.All)
            => ClientHelper.GetStreamAsync(this, id, type);
        public Task<IEnumerable<RestStream>> FindStreamsAsync(string query)
            => FindStreamsAsync(query, true);
        public Task<IEnumerable<RestStream>> FindStreamsAsync(string query, bool hls = true, PageOptions options = null)
            => ClientHelper.FindStreamsAsync(this, query, hls, options);
        public Task<IEnumerable<RestStream>> GetStreamsAsync()
            => GetStreamsAsync(null);
        public Task<IEnumerable<RestStream>> GetStreamsAsync(string game = null, uint[] channelids = null, string language = null, StreamType type = StreamType.All, PageOptions options = null)
            => ClientHelper.GetStreamsAsync(this, game, channelids, language, type, options);
        public Task<IEnumerable<RestFeaturedStream>> GetFeaturedStreamsAsync()
            => GetFeaturedStreamsAsync(null);
        public Task<IEnumerable<RestFeaturedStream>> GetFeaturedStreamsAsync(PageOptions options = null)
            => ClientHelper.GetFeaturedStreamsAsync(this, options);
        public Task<RestStreamSummary> GetStreamSummaryAsync(string game)
            => ClientHelper.GetStreamSummaryAsync(this, game);
        public Task<IEnumerable<RestTeamSummary>> GetTeamsAsync()
            => GetTeamsAsync(null);
        public Task<IEnumerable<RestTeamSummary>> GetTeamsAsync(PageOptions options = null)
            => ClientHelper.GetTeamsAsync(this, options);
        public Task<RestTeam> GetTeamAsync(string name)
            => ClientHelper.GetTeamAsync(this, name);
        public Task<RestUser> GetUserAsync(uint id)
            => ClientHelper.GetUserAsync(this, id);
        public Task<RestUser> FindUserAsync(string name)
            => ClientHelper.FindUserAsync(this, name);
        public Task<IEnumerable<RestVideo>> GetTopVideosAsync()
            => GetTopVideosAsync(null);
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
