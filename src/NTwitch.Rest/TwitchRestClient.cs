using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public class TwitchRestClient : ITwitchClient
    {
        private RestApiClient _rest;
        public string BaseUrl { get; }
        
        public TwitchRestClient() : this(new TwitchRestClientConfig()) { }
        public TwitchRestClient(TwitchRestClientConfig config)
        {
            BaseUrl = config.BaseUrl;
        }

        /// <summary> Get information about a user. </summary>
        public async Task<RestUser> GetUserAsync(string name)
            => await _rest.SendAsync<RestUser>("GET", "users/" + name);

        /// <summary> Get information about the current user. </summary>
        public async Task<RestSelfUser> GetCurrentUserAsync()
            => await _rest.SendAsync<RestSelfUser>("GET", "user");

        /// <summary> Find games related to a query. </summary>
        public async Task<IEnumerable<RestGame>> FindGamesAsync(string query, bool? islive = null)
            => await _rest.SendAsync<IEnumerable<RestGame>>("GET", "search/games");

        /// <summary> Get the top streamed games on Twitch. </summary>
        public async Task GetTopGamesAsync()
            => await _rest.SendAsync<RestTopGame>("GET", "games/top");

        /// <summary> Find streams related to a query. </summary>
        public async Task<IEnumerable<RestStream>> FindStreamsAsync(string query, bool? hls = null, int limit = 25, int page = 1)
            => await _rest.SendAsync<IEnumerable<RestStream>>("GET", "search/streams");

        /// <summary> Get all streams on Twitch. </summary>
        public async Task GetStreamsAsync(string game = null, string channel = null, string language = null, StreamType type = StreamType.All, int limit = 25, int page = 1)
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
        public async Task<IEnumerable<RestVideo>> GetTopVideosAsync(string game = null, VideoPeriod period = VideoPeriod.Week, int limit = 10, int page = 1)
            => await _rest.SendAsync<IEnumerable<RestVideo>>("GET", "videos/top");

        /// <summary> Find channels related to a query. </summary>
        public async Task<IEnumerable<RestChannel>> FindChannelsAsync(string query, int limit = 25, int page = 1)
            => await _rest.SendAsync<IEnumerable<RestChannel>>("GET", "search/channels");

        // ITwitchClient
        public ConnectionState ConnectionState { get; private set; } = ConnectionState.Disconnected;

        /// <summary> Authenticate this client with the twitch oauth servers. </summary>
        public async Task LoginAsync(string clientid, string token = null)
        {
            _rest = new RestApiClient(BaseUrl, clientid, token);
            await Task.Delay(1);
            //var info = await _rest.LoginAsync(token);
        }
    }
}
