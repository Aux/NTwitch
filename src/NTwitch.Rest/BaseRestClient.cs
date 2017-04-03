using NTwitch.Rest.API;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public partial class BaseRestClient
    {
        /// <summary> The client used to manage rest requests </summary>
        public RestApiClient RestClient => _rest;

        internal ConcurrentDictionary<ulong, RestTokenInfo> Tokens;
        internal LogManager Logger => _rest.Logger;

        private RestApiClient _rest;

        public BaseRestClient(TwitchRestConfig config)
        {
            _rest = new RestApiClient(config);
            Logger.LogReceived += OnLogInternalAsync;
        }
        
        private Task OnLogInternalAsync(LogMessage msg)
            => logEvent.InvokeAsync(msg);

        internal async Task<RestTokenInfo> RestLoginAsync(string token)
        {
            var auth = await RestHelper.AuthorizeAsync(this, token);
            Tokens.AddOrUpdate(auth.UserId.Value, auth, (id, t) => t);
            await loggedInEvent.InvokeAsync(auth).ConfigureAwait(false);
            return auth;
        }

        // Tokens
        public RestTokenInfo GetTokenInfo(ulong userId)
            => RestHelper.GetTokenInfo(this, userId);

        // Search
        /// <summary> Find channels relating to the specified query </summary>
        public Task<IReadOnlyCollection<RestChannel>> SearchChannelsAsync(string query, uint limit = 25, uint offset = 0)
            => RestHelper.SearchChannelsAsync(this, query, limit, offset);
        /// <summary> Find games relating to the specified query </summary>
        public Task<IReadOnlyCollection<RestGame>> SearchGamesAsync(string query, bool islive = false)
            => RestHelper.SearchGamesAsync(this, query, islive);
        /// <summary> Find streams relating to the specified query </summary>
        public Task<IReadOnlyCollection<RestStream>> SearchStreamsAsync(string query, bool? hls = null, uint limit = 25, uint offset = 0)
            => RestHelper.SearchStreamsAsync(this, query, hls, limit, offset);

        // User
        /// <summary> Get the user associated with the authorized token </summary>
        public Task<RestSelfUser> GetSelfUserAsync(ulong userId)
            => RestHelper.GetSelfUserAsync(this, userId);
        /// <summary> Get information about a user by id </summary>
        public Task<RestUser> GetUserAsync(ulong userId)
            => RestHelper.GetUserAsync(this, userId);
        /// <summary> Get information about users by name </summary>
        public Task<IReadOnlyCollection<RestUser>> GetUsersAsync(params string[] usernames)
            => RestHelper.GetUsersAsync(this, usernames);

        // Channel
        /// <summary> Get the channel associated with the authorized token </summary>
        public Task<RestSelfChannel> GetSelfChannelAsync(ulong channelId)
            => RestHelper.GetSelfChannelAsync(this, channelId);
        /// <summary> Get information about a channel by id </summary>
        public Task<RestChannel> GetChannelAsync(ulong channelId)
            => RestHelper.GetChannelAsync(this, channelId);

        // Streams
        /// <summary> Get information about a channel's stream </summary>
        public Task<RestStream> GetStreamAsync(ulong channelId, StreamType type = StreamType.Live)
            => RestHelper.GetStreamAsync(this, channelId, type);
        /// <summary> Get the top viewed streams on twitch for the specified options </summary>
        public Task<IReadOnlyCollection<RestStream>> GetStreamsAsync(Action<GetStreamsParams> options)
            => RestHelper.GetStreamsAsync(this, options);
        /// <summary> Get a summary of popularity for the specified game </summary>
        public Task<RestGameSummary> GetGameSummaryAsync(string game)
            => RestHelper.GetGameSummaryAsync(this, game);
        /// <summary> Get the streams that appear on the front page of twitch </summary>
        public Task<IReadOnlyCollection<RestFeaturedStream>> GetFeaturedStreamsAsync(uint limit = 25, uint offset = 0)
            => RestHelper.GetFeaturedStreamsAsync(this, limit, offset);

        // Teams
        /// <summary> Get all teams on twitch </summary>
        public Task<IReadOnlyCollection<RestSimpleTeam>> GetTeamsAsync(uint limit = 25, uint offset = 0)
            => RestHelper.GetTeamsAsync(this, limit, offset);
        /// <summary> Get a team by name </summary>
        public Task<RestTeam> GetTeamAsync(string name)
            => RestHelper.GetTeamAsync(this, name);

        // Community
        /// <summary> Get information about a community by id </summary>
        public Task<RestCommunity> GetCommunityAsync(string communityId, bool isname = false)
            => RestHelper.GetCommunityAsync(this, communityId, isname);
        /// <summary> Get the most popular communities on twitch </summary>
        public Task<IReadOnlyCollection<RestTopCommunity>> GetTopCommunitiesAsync(uint limit = 10)
            => RestHelper.GetTopCommunitiesAsync(this, limit);

        // Videos
        /// <summary> Get information about a video by id </summary>
        public Task<RestVideo> GetVideoAsync(string videoId)
            => RestHelper.GetVideoAsync(this, videoId);

        // Ingests
        /// <summary> Get information about twitch's ingest servers </summary>
        public Task<IReadOnlyCollection<RestIngest>> GetIngestsAsync()
            => RestHelper.GetIngestsAsync(this);
        
    }
}
