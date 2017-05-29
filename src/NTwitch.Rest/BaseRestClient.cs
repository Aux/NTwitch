using NTwitch.Rest.API;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public partial class BaseRestClient
    {
        public RestApiClient RestClient => _rest;
        public IReadOnlyCollection<RestTokenInfo> TokenInfos => Tokens.Select(x => x.Value).ToArray();

        internal ConcurrentDictionary<ulong, RestTokenInfo> Tokens;
        internal LogManager Logger => _rest.Logger;

        private RestApiClient _rest;

        public BaseRestClient(TwitchRestConfig config)
        {
            Tokens = new ConcurrentDictionary<ulong, RestTokenInfo>();
            _rest = new RestApiClient(config);
            Logger.LogReceived += OnLogInternalAsync;
        }
        
        internal Task OnLogInternalAsync(LogMessage msg)
            => logEvent.InvokeAsync(msg);

        internal async Task<RestTokenInfo> RestLoginAsync(string token)
        {
            var auth = await RestHelper.AuthorizeAsync(this, token);
            Tokens.AddOrUpdate(auth.UserId, auth, (id, t) => t);
            return auth;
        }

        // Tokens
        public RestTokenInfo GetTokenInfo(ulong userId)
            => RestHelper.GetTokenInfo(this, userId);

        // Clips
        /// <summary> Get information about a clip by id </summary>
        public Task<RestClip> GetClipAsync(string clipId)
            => RestHelper.GetClipAsync(this, clipId);
        /// <summary> Get the most popular clips for the specified parameters </summary>
        public Task<IReadOnlyCollection<RestClip>> GetTopClipsAsync(Action<TopClipsParams> options)
            => RestHelper.GetTopClipsAsync(this, options);

        // Community
        /// <summary> Get information about a community by id </summary>
        public Task<RestCommunity> GetCommunityAsync(string communityId, bool isname = false)
            => RestHelper.GetCommunityAsync(this, communityId, isname);
        /// <summary> Get the most popular communities on twitch </summary>
        public Task<IReadOnlyCollection<RestTopCommunity>> GetTopCommunitiesAsync(uint limit = 10)
            => RestHelper.GetTopCommunitiesAsync(this, limit);

        // Channel
        /// <summary> Get the channel associated with the authorized token </summary>
        public Task<RestSelfChannel> GetSelfChannelAsync(ulong channelId)
            => RestHelper.GetSelfChannelAsync(this, channelId);
        /// <summary> Get information about a channel by id </summary>
        public Task<RestChannel> GetChannelAsync(ulong channelId)
            => RestHelper.GetChannelAsync(this, channelId);
        
        // Ingests
        /// <summary> Get information about twitch's ingest servers </summary>
        public Task<IReadOnlyCollection<RestIngest>> GetIngestsAsync()
            => RestHelper.GetIngestsAsync(this);

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

        // Streams
        /// <summary> Get information about a channel's stream </summary>
        public Task<RestStream> GetStreamAsync(ulong channelId, StreamType type = StreamType.Live)
            => RestHelper.GetStreamAsync(this, channelId, type);
        /// <summary> Get the streams for the specified channels, if available </summary>
        public Task<IReadOnlyCollection<RestStream>> GetStreamsAsync(params ulong[] channels)
            => RestHelper.GetStreamsAsync(this, channels);
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

        // Videos
        /// <summary> Get information about a video by id </summary>
        public Task<RestVideo> GetVideoAsync(string videoId)
            => RestHelper.GetVideoAsync(this, videoId);
    }
}
