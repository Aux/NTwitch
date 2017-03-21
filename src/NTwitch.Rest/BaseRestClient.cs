using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public partial class BaseRestClient
    {
        public RestApiClient RestClient => _rest;
        public RestToken Token => _auth;

        private LogManager _log;
        private RestApiClient _rest;
        private RestToken _auth;
        private TwitchRestConfig _config;

        public BaseRestClient(TwitchRestConfig config)
        {
            _log = new LogManager(config.LogLevel);
            _config = config;

            _log.LogReceived += OnLogInternalAsync;
        }

        private Task OnLogInternalAsync(LogMessage msg)
            => logEvent.InvokeAsync(msg);

        internal async Task RestLoginAsync(TokenType type, string token)
        {
            await _log.InfoAsync("Rest", "Logging in...").ConfigureAwait(false);
            _rest = new RestApiClient(_config, _log, type, token);
            var auth = await _rest.ValidateTokenAsync();
            _auth = RestToken.Create(auth);
            await _log.InfoAsync("Rest", "Login success!").ConfigureAwait(false);
            await loggedInEvent.InvokeAsync().ConfigureAwait(false);
        }

        // User
        public Task<RestSelfUser> GetCurrentUserAsync()
            => ClientHelper.GetCurrentUserAsync(this);
        public Task<RestUser> GetUserAsync(ulong id)
            => ClientHelper.GetUserAsync(this, id);
        public Task<IEnumerable<RestUser>> GetUsersAsync(params string[] usernames)
            => ClientHelper.GetUsersAsync(this, usernames);

        // Channel
        public Task<RestSelfChannel> GetCurrentChannelAsync()
            => ClientHelper.GetCurrentChannelAsync(this);
        public Task<RestChannel> GetChannelAsync(ulong channelId)
            => ClientHelper.GetChannelAsync(this, channelId);
        public Task<IEnumerable<RestCheerInfo>> GetCheersAsync(ulong channelId)
            => ClientHelper.GetCheersAsync(this, channelId);

        // Community
        public Task<RestCommunity> GetCommunityAsync(string id, bool isname = false)
            => ClientHelper.GetCommunityAsync(this, id, isname);

        // Videos
        public Task<RestVideo> GetVideoAsync(string id)
            => ClientHelper.GetVideoAsync(this, id);
        
    }
}
