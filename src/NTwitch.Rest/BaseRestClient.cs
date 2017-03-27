using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public partial class BaseRestClient
    {
        public RestApiClient RestClient => _rest;
        public RestTokenInfo Token => _auth;

        internal LogManager Logger;

        private RestApiClient _rest;
        private RestTokenInfo _auth;
        private TwitchRestConfig _config;

        public BaseRestClient(TwitchRestConfig config)
        {
            Logger = new LogManager(config.LogLevel);
            _config = config;

            Logger.LogReceived += OnLogInternalAsync;
        }
        
        private Task OnLogInternalAsync(LogMessage msg)
            => logEvent.InvokeAsync(msg);

        internal async Task RestLoginAsync(AuthMode type, string token)
        {
            _rest = new RestApiClient(_config, Logger, type, token);
            _auth = await ClientHelper.AuthorizeAsync(this);
            await loggedInEvent.InvokeAsync(_auth).ConfigureAwait(false);
        }

        // Search
        public Task<IReadOnlyCollection<RestChannel>> SearchChannelsAsync(string query, uint limit = 25, uint offset = 0)
            => ClientHelper.SearchChannelsAsync(this, query, limit, offset);
        public Task<IReadOnlyCollection<RestGame>> SearchGamesAsync(string query, bool islive = false)
            => ClientHelper.SearchGamesAsync(this, query, islive);
        public Task<IReadOnlyCollection<RestStream>> SearchStreamsAsync(string query, bool? hls = null, uint limit = 25, uint offset = 0)
            => ClientHelper.SearchStreamsAsync(this, query, hls, limit, offset);

        // User
        public Task<RestSelfUser> GetCurrentUserAsync()
            => ClientHelper.GetCurrentUserAsync(this);
        public Task<RestUser> GetUserAsync(ulong userId)
            => ClientHelper.GetUserAsync(this, userId);
        public Task<IReadOnlyCollection<RestUser>> GetUsersAsync(params string[] usernames)
            => ClientHelper.GetUsersAsync(this, usernames);

        // Channel
        public Task<RestSelfChannel> GetCurrentChannelAsync()
            => ClientHelper.GetCurrentChannelAsync(this);
        public Task<RestChannel> GetChannelAsync(ulong channelId)
            => ClientHelper.GetChannelAsync(this, channelId);
        public Task<IReadOnlyCollection<RestCheerInfo>> GetCheersAsync(ulong channelId)
            => ClientHelper.GetCheersAsync(this, channelId);

        // Streams
        public Task<IReadOnlyCollection<RestStream>> GetFollowedStreamsAsync(StreamType type = StreamType.Live, uint limit = 25, uint offset = 0)
            => ClientHelper.GetFollowedStreamsAsync(this, type, limit, offset);
        public Task<RestStream> GetStreamAsync(ulong channelId, StreamType type = StreamType.Live)
            => ClientHelper.GetStreamAsync(this, channelId, type);
        public Task<IReadOnlyCollection<RestStream>> GetStreamsAsync(Action<GetStreamsParams> options)
            => ClientHelper.GetStreamsAsync(this, options);
        public Task<RestStreamSummary> GetStreamSummaryAsync(string game)
            => ClientHelper.GetStreamSummaryAsync(this, game);
        public Task<IReadOnlyCollection<RestFeaturedStream>> GetFeaturedStreamsAsync(uint limit = 25, uint offset = 0)
            => ClientHelper.GetFeaturedStreamsAsync(this, limit, offset);

        // Teams
        public Task<IReadOnlyCollection<RestSimpleTeam>> GetTeamsAsync(uint limit = 25, uint offset = 0)
            => ClientHelper.GetTeamsAsync(this, limit, offset);
        public Task<RestTeam> GetTeamAsync(string name)
            => ClientHelper.GetTeamAsync(this, name);

        // Community
        public Task<RestCommunity> GetCommunityAsync(string communityId, bool isname = false)
            => ClientHelper.GetCommunityAsync(this, communityId, isname);
        public Task<IReadOnlyCollection<RestTopCommunity>> GetTopCommunitiesAsync(uint limit = 10)
            => ClientHelper.GetTopCommunitiesAsync(this, limit);

        // Videos
        public Task<RestVideo> GetVideoAsync(string videoId)
            => ClientHelper.GetVideoAsync(this, videoId);

        // Ingests
        public Task<IReadOnlyCollection<RestIngest>> GetIngestsAsync()
            => ClientHelper.GetIngestsAsync(this);
        
    }
}
