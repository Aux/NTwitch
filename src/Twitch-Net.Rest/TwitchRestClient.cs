using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Twitch.Rest
{
    public class TwitchRestClient : ITwitchClient
    {
        private TwitchRestClientConfig _config;
        private RestApiClient _rest;
        private string _token;
        
        public TwitchRestClient() { }
        public TwitchRestClient(TwitchRestClientConfig config)
        {
            _config = config;
        }

        /// <summary> Authenticate this client with the twitch oauth servers. </summary>
        public async Task LoginAsync(string token)
            => await _rest.LoginAsync(token);

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
        public async Task<IEnumerable<IStream>> FindStreamsAsync(string query, bool? hls = null, int limit = 25, int page = 1)
            => await _rest.SendAsync<IEnumerable<IStream>>("GET", "search/streams");

        /// <summary> Get all streams on Twitch. </summary>
        public async Task GetStreamsAsync(string game = null, string channel = null, string language = null, StreamType type = StreamType.All, int limit = 25, int page = 1)
            => await _rest.SendAsync<IStream>("GET", "streams");

        /// <summary> Get featured (promoted) streams on Twitch. </summary>
        public async Task GetFeaturedStreamsAsync(int limit = 25, int page = 1)
            => await _rest.SendAsync<IStream>("GET", "streams/featured");

        /// <summary> Get featured (promoted) streams on Twitch. </summary>
        public async Task GetStreamSummaryAsync(string game = null)
            => await _rest.SendAsync<IStreamSummary>("GET", "streams/summary");

        /// <summary> Get a specific video by id. </summary>
        public async Task<IVideo> GetVideoAsync(string id)
            => await _rest.SendAsync<IVideo>("GET", "videos/" + id);
        
        /// <summary> Get the top videos in the specified period. </summary>
        public async Task<IEnumerable<IVideo>> GetTopVideosAsync(string game = null, VideoPeriod period = VideoPeriod.Week, int limit = 10, int page = 1)
            => await _rest.SendAsync<IEnumerable<IVideo>>("GET", "videos/top");

        /// <summary> Find channels related to a query. </summary>
        public async Task<IEnumerable<IChannel>> FindChannelsAsync(string query, int limit = 25, int page = 1)
            => await _rest.SendAsync<IEnumerable<IChannel>>("GET", "search/channels");

        // ITwitchClient
        public ConnectionState ConnectionState { get; }

        public Task ConnectAsync()
        {
            throw new NotImplementedException();
        }

        public Task DisconnectAsync()
        {
            throw new NotImplementedException();
        }
    }
}
