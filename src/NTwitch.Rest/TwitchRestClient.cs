using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public partial class TwitchRestClient : ITwitchClient
    {
        private RestApiClient _rest;
        private LogManager _log;
        public string BaseUrl { get; }
        
        public TwitchRestClient() : this(new TwitchRestConfig()) { }
        public TwitchRestClient(TwitchRestConfig config)
        {
            BaseUrl = config.BaseUrl;

            _log = new LogManager(config.LogLevel);
            _log.LogReceived += OnLogReceived;
        }

        private async Task OnLogReceived(LogMessage msg)
            => await _logEvent.InvokeAsync(msg);

        /// <summary> Get information about a user. </summary>
        public async Task<RestUser> FindUserAsync(string name)
            => await _rest.SendAsync<RestUser>("GET", "users?login=" + name);
        
        /// <summary> Get information about the current user. </summary>
        /// <remarks> Requires scope: `user_read` </remarks>
        public async Task<RestSelfUser> GetCurrentUserAsync()
            => await _rest.SendAsync<RestSelfUser>("GET", "user");

        /// <summary> Find games related to a query. </summary>
        public async Task<IEnumerable<RestGame>> FindGamesAsync(string query, bool? islive = null)
            => await _rest.SendAsync<IEnumerable<RestGame>>("GET", "search/games?q=" + query);

        /// <summary> Get the top streamed games on Twitch. </summary>
        public async Task<IEnumerable<RestTopGame>> GetTopGamesAsync(TwitchPagination options = null)
            => (await _rest.SendAsync<RestTopGameCollection>("GET", "games/top", options)).Games;
        
        /// <summary> Find streams related to a query. </summary>
        public async Task<IEnumerable<RestStream>> FindStreamsAsync(string query, bool? hls = null, TwitchPagination options = null)
            => await _rest.SendAsync<IEnumerable<RestStream>>("GET", "search/streams?q=" + query);

        /// <summary> Get all streams on Twitch. </summary>
        public async Task GetStreamsAsync(string game = null, string channel = null, string language = null, StreamType type = StreamType.All, TwitchPagination options = null)
            => await _rest.SendAsync<RestStream>("GET", "streams");

        /// <summary> Get featured (promoted) streams on Twitch. </summary>
        public async Task GetFeaturedStreamsAsync(int limit = 25, int page = 1)
            => await _rest.SendAsync<RestStream>("GET", "streams/featured");

        /// <summary> Get featured (promoted) streams on Twitch. </summary>
        public async Task GetStreamSummaryAsync(string game = null)
            => await _rest.SendAsync<RestStreamSummary>("GET", "streams/summary");

        /// <summary> Get a specific video by id. </summary>
        public async Task<RestVideo> GetVideoAsync(string id)
            => await _rest.SendAsync<RestVideo>("GET", "videos/" + id);
        
        /// <summary> Get the top videos in the specified period. </summary>
        public async Task<IEnumerable<RestVideo>> GetTopVideosAsync(string game = null, VideoPeriod period = VideoPeriod.Week, TwitchPagination options = null)
            => await _rest.SendAsync<IEnumerable<RestVideo>>("GET", "videos/top");

        /// <summary> Find channels related to a query. </summary>
        public async Task<IEnumerable<RestChannel>> FindChannelsAsync(string query, TwitchPagination options = null)
            => await _rest.SendAsync<IEnumerable<RestChannel>>("GET", "search/channels?q=" + query);

        // ITwitchClient
        public ConnectionState ConnectionState { get; private set; } = ConnectionState.Disconnected;

        /// <summary> Authenticate this client with the twitch oauth servers. </summary>
        public async Task LoginAsync(string clientid, string token = null)
        {
            if (string.IsNullOrWhiteSpace(clientid))
                throw new ArgumentNullException("clientid");

            _rest = new RestApiClient(_log, BaseUrl, clientid, token);
            await _log.DebugAsync("TwitchRestClient", "RestApiClient created successfully");

            if (!string.IsNullOrWhiteSpace(token))
            {
                await _log.DebugAsync("TwitchRestClient", "Validating token");
                await _rest.LoginAsync(token);
            }
        }
    }
}
