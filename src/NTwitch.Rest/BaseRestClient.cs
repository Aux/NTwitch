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

        internal async Task LoginInternalAsync(TokenType type, string token)
        {
            _rest = new RestApiClient(_config, type, token);
            var auth = await _rest.ValidateTokenAsync();
            _auth = RestToken.Create(auth);
        }

        // User
        public Task<RestUser> GetUserAsync(ulong id)
            => ClientHelper.GetUserAsync(this, id);
    }
}
