using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    /// <summary> A Rest-only twitch client. </summary>
    public partial class TwitchRestClient : BaseTwitchClient, ITwitchClient
    {
        public RestApiClient ApiClient => _rest;

        private RestApiClient _rest;
        private string _apiurl;
        
        public TwitchRestClient() : this(new TwitchRestConfig()) { }
        public TwitchRestClient(TwitchRestConfig config) : base(config)
        {
            _apiurl = config.ApiUrl;
        }

        public async Task LoginAsync(string clientid, string token = null)
        {
            if (string.IsNullOrWhiteSpace(clientid))
                throw new ArgumentNullException("clientid");

            _rest = new RestApiClient(_log, _apiurl, clientid, token);
            await _log.DebugAsync("Rest", "Api client created successfully");
        }

        public Task<RestSelfUser> GetCurrentUserAsync()
            => ClientHelper.GetCurrentUserAsync(this);

        public Task<RestSelfChannel> GetCurrentChannelAsync()
            => ClientHelper.GetCurrentChannelAsync(this);
        
        public Task<RestChannel> GetChannelAsync(ulong id)
            => ClientHelper.GetChannelAsync(this, id);
        
        public Task<IEnumerable<RestTopGame>> GetTopGamesAsync(TwitchPageOptions options = null)
            => ClientHelper.GetTopGamesAsync(this, options);
        
        public Task<IEnumerable<RestIngest>> GetIngestsAsync()
            => ClientHelper.GetIngestsAsync(this);
        
        public Task<IEnumerable<RestChannel>> FindChannelsAsync(string query, TwitchPageOptions options = null)
            => ClientHelper.FindChannelsAsync(this, query, options);
        
        public Task<IEnumerable<RestGame>> FindGamesAsync(string query, bool islive = true)
            => ClientHelper.FindGamesAsync(this, query, islive);
        
        public Task<RestStream> GetStreamAsync(ulong id, StreamType type = StreamType.All)
            => ClientHelper.GetStreamAsync(this, id, type);
        
        public Task<IEnumerable<RestStream>> FindStreamsAsync(string query, bool hls = true, TwitchPageOptions options = null)
            => ClientHelper.FindStreamsAsync(this, query, hls, options);
        
        public Task<IEnumerable<RestStream>> GetStreamsAsync(string game = null, ulong[] channelids = null, string language = null, StreamType type = StreamType.All, TwitchPageOptions options = null)
            => ClientHelper.GetStreamsAsync(this, game, channelids, language, type, options);
        
        public Task<IEnumerable<RestFeaturedStream>> GetFeaturedStreamsAsync(TwitchPageOptions options = null)
            => ClientHelper.GetFeaturedStreamsAsync(this, options);
        
        public Task<RestStreamSummary> GetStreamSummaryAsync(string game)
            => ClientHelper.GetStreamSummaryAsync(this, game);
        
        public Task<IEnumerable<RestTeamSummary>> GetTeamsAsync(TwitchPageOptions options = null)
            => ClientHelper.GetTeamsAsync(this, options);
        
        public Task<RestTeam> GetTeamAsync(string name)
            => ClientHelper.GetTeamAsync(this, name);
        
        public Task<RestUser> GetUserAsync(ulong id)
            => ClientHelper.GetUserAsync(this, id);
        
        public Task<IEnumerable<RestUser>> FindUsersAsync(string name)
            => ClientHelper.FindUsersAsync(this, name);
        
        public Task<IEnumerable<RestVideo>> GetTopVideosAsync(string game = null, VideoPeriod period = VideoPeriod.Week, BroadcastType type = BroadcastType.Highlight, TwitchPageOptions options = null)
            => ClientHelper.GetTopVideosAsync(this, game, period, type, options);
    }
}
