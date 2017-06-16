using NTwitch.Rest.API;

namespace NTwitch.Rest
{
    public class TwitchRestClient : BaseTwitchClient, ITwitchClient
    {
        public new RestTokenInfo TokenInfo => base.TokenInfo as RestTokenInfo;

        public TwitchRestClient() : this(new TwitchRestConfig()) { }
        public TwitchRestClient(TwitchRestConfig config) 
            : base(config, CreateApiClient(config)) { }

        private static TwitchRestApiClient CreateApiClient(TwitchRestConfig config)
            => new TwitchRestApiClient(config.RestClientProvider, config.ClientId, TwitchConfig.UserAgent);

        internal override void Dispose(bool disposing)
        {
            if (disposing)
                ApiClient.Dispose();
        }
    }
}
