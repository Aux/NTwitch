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

        /// <summary> Get information about the authenticated user. </summary>
        public Task<RestSelfUser> GetCurrentUserAsync()
            => ClientHelper.GetCurrentUserAsync(this);

        /// <summary> Get information about the authenticated user's channel. </summary>
        public Task<RestSelfChannel> GetCurrentChannelAsync()
            => ClientHelper.GetCurrentChannelAsync(this);

        /// <summary> Get information about the specified channel. </summary>
        public Task<RestChannel> GetChannelAsync(ulong id)
            => ClientHelper.GetChannelAsync(this, id);

        /// <summary> Get a collection of the current most viewed games on twitch. </summary>
        public Task<IEnumerable<RestTopGame>> GetTopGamesAsync()
            => GetTopGamesAsync(null);

        /// <summary> Get a collection of the current most viewed games on twitch. </summary>
        public Task<IEnumerable<RestTopGame>> GetTopGamesAsync(PageOptions options = null)
            => ClientHelper.GetTopGamesAsync(this, options);

        /// <summary> Get a collection of available twitch ingest servers. </summary>
        public Task<IEnumerable<RestIngest>> GetIngestsAsync()
            => ClientHelper.GetIngestsAsync(this);

        /// <summary> Get a collection of channels related to the provided query. </summary>
        public Task<IEnumerable<RestChannel>> FindChannelsAsync(string query)
            => FindChannelsAsync(query, null);

        /// <summary> Get a collection of channels related to the provided query. </summary>
        public Task<IEnumerable<RestChannel>> FindChannelsAsync(string query, PageOptions options = null)
            => ClientHelper.FindChannelsAsync(this, query, options);

        /// <summary> Get a collection of games related to the provided query. </summary>
        public Task<IEnumerable<RestGame>> FindGamesAsync(string query)
            => FindGamesAsync(query, true);

        /// <summary> Get a collection of games related to the provided query. </summary>
        public Task<IEnumerable<RestGame>> FindGamesAsync(string query, bool? islive = null)
            => ClientHelper.FindGamesAsync(this, query, islive);

        /// <summary> Get information about a specified stream. </summary>
        public Task<RestStream> GetStreamAsync(ulong id)
            => GetStreamAsync(id, StreamType.All);

        /// <summary> Get information about a specified stream. </summary>
        public Task<RestStream> GetStreamAsync(ulong id, StreamType type = StreamType.All)
            => ClientHelper.GetStreamAsync(this, id, type);

        /// <summary> Get a collection of streams related to the provided query. </summary>
        public Task<IEnumerable<RestStream>> FindStreamsAsync(string query)
            => FindStreamsAsync(query, true);

        /// <summary> Get a collection of streams related to the provided query. </summary>
        public Task<IEnumerable<RestStream>> FindStreamsAsync(string query, bool hls = true, PageOptions options = null)
            => ClientHelper.FindStreamsAsync(this, query, hls, options);

        /// <summary>  </summary>
        public Task<IEnumerable<RestStream>> GetStreamsAsync()
            => GetStreamsAsync(null);

        /// <summary>  </summary>
        public Task<IEnumerable<RestStream>> GetStreamsAsync(string game = null, uint[] channelids = null, string language = null, StreamType type = StreamType.All, PageOptions options = null)
            => ClientHelper.GetStreamsAsync(this, game, channelids, language, type, options);

        /// <summary> Get a collection of streams featured on twitch's home page. </summary>
        public Task<IEnumerable<RestFeaturedStream>> GetFeaturedStreamsAsync()
            => GetFeaturedStreamsAsync(null);

        /// <summary> Get a collection of streams featured on twitch's home page. </summary>
        public Task<IEnumerable<RestFeaturedStream>> GetFeaturedStreamsAsync(PageOptions options = null)
            => ClientHelper.GetFeaturedStreamsAsync(this, options);

        /// <summary> Get the summary of all streams playing a game. </summary>
        public Task<RestStreamSummary> GetStreamSummaryAsync(string game)
            => ClientHelper.GetStreamSummaryAsync(this, game);

        /// <summary>  </summary>
        public Task<IEnumerable<RestTeamSummary>> GetTeamsAsync()
            => GetTeamsAsync(null);

        /// <summary>  </summary>
        public Task<IEnumerable<RestTeamSummary>> GetTeamsAsync(PageOptions options = null)
            => ClientHelper.GetTeamsAsync(this, options);

        /// <summary> Get information about a specified team by name. </summary>
        public Task<RestTeam> GetTeamAsync(string name)
            => ClientHelper.GetTeamAsync(this, name);

        /// <summary> Get information about the specified user. </summary>
        public Task<RestUser> GetUserAsync(ulong id)
            => ClientHelper.GetUserAsync(this, id);

        /// <summary> Get information about a specified user, by name. </summary>
        public Task<RestUser> FindUserAsync(string name)
            => ClientHelper.FindUserAsync(this, name);

        /// <summary>  </summary>
        public Task<IEnumerable<RestVideo>> GetTopVideosAsync()
            => GetTopVideosAsync(null);

        /// <summary>  </summary>
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
