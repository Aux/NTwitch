using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public class BaseRestClient
    {
        public RestApiClient RestClient => _rest;
        public RestToken Token => _auth;

        internal RestApiClient _rest;

        private RestToken _auth;
        private TwitchRestConfig _config;

        public BaseRestClient(TwitchRestConfig config)
        {
            _config = config;
        }

        internal async Task RestLoginAsync(TokenType type, string token)
        {
            _rest = new RestApiClient(_config, type, token);
            var auth = await _rest.ValidateTokenAsync();
            _auth = RestToken.Create(auth);
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
