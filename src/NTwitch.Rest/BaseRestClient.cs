using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public partial class BaseRestClient
    {
        //public IReadOnlyDictionary<ulong, RestTokenInfo> Tokens => _tokens;
        public RestApiClient RestClient => _rest;
        public RestTokenInfo Token => _auth;

        internal LogManager Logger;

        //private ConcurrentDictionary<ulong, RestTokenInfo> _tokens;
        private RestApiClient _rest;
        private RestTokenInfo _auth;
        private TwitchRestConfig _config;

        public BaseRestClient(TwitchRestConfig config)
        {
            //_tokens = new ConcurrentDictionary<ulong, RestTokenInfo>();
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
            //_tokens.AddOrUpdate(auth.UserId.Value, auth, (i, f) => f);
            await loggedInEvent.InvokeAsync(_auth).ConfigureAwait(false);
        }

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
