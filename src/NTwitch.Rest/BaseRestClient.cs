using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public class BaseRestClient
    {
        public RestApiClient Client => _client;
        public RestToken Token => _auth;

        internal RestApiClient _client;

        private RestToken _auth;
        private string _host;

        public BaseRestClient(TwitchRestConfig config)
        {
            _host = config.Host;
        }

        internal async Task LoginInternalAsync(TokenType type, string token)
        {
            _client = new RestApiClient(_host, type, token);
            var auth = await _client.ValidateTokenAsync();
            _auth = RestToken.Create(auth);
        }

        // User
        public Task<RestUser> GetUserAsync(ulong id)
            => ClientHelper.GetUserAsync(this, id);
    }
}
