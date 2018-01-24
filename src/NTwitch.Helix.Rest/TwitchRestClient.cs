using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch.Helix.Rest
{
    public class TwitchRestClient : BaseTwitchClient, ITwitchClient
    {
        public new RestSelfUser CurrentUser => base.CurrentUser as RestSelfUser;

        public TwitchRestClient() : this(new TwitchRestConfig()) { }
        public TwitchRestClient(TwitchRestConfig config) : base(config, CreateApiClient(config)) { }

        private static API.TwitchRestApiClient CreateApiClient(TwitchRestConfig config)
            => new API.TwitchRestApiClient(config.RestClientProvider, config.ClientId, TwitchConfig.UserAgent);
        internal override void Dispose(bool disposing)
        {
            if (disposing)
                ApiClient.Dispose();
        }

        //internal override async Task OnLoginAsync(string token)
        //{
        //    var models = await ApiClient.GetUsersAsync(null, null, new RequestOptions { RetryMode = RetryMode.AlwaysRetry }).ConfigureAwait(false);
        //    var user = models.SingleOrDefault();
        //    ApiClient.CurrentUserId = user.Id;
        //    base.CurrentUser = RestSelfUser.Create(this, user);
        //}

        // Broadcasts
        public IAsyncEnumerable<IReadOnlyCollection<RestBroadcast>> GetBroadcastsAsync(string[] communityIds = null, string[] languages = null, ulong[] userIds = null, string[] userNames = null, BroadcastType? broadcastType = null, int limit = 20, RequestOptions options = null)
            => ClientHelper.GetBroadcastsAsync(this, communityIds, languages, userIds, userNames, broadcastType, limit, options);
        
        // Users
        public Task<IReadOnlyCollection<RestUser>> GetUsersAsync(ulong[] userIds = null, string[] userNames = null, RequestOptions options = null)
            => ClientHelper.GetUsersAsync(this, userIds, userNames, options);
    }
}
