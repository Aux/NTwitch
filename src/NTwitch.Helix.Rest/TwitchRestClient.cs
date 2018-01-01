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
            => new API.TwitchRestApiClient(config.RestClientProvider, TwitchConfig.UserAgent);
        internal override void Dispose(bool disposing)
        {
            if (disposing)
                ApiClient.Dispose();
        }

        internal override async Task OnLoginAsync(string token)
        {
            var user = await ApiClient.GetMyUserAsync(new RequestOptions { RetryMode = RetryMode.AlwaysRetry }).ConfigureAwait(false);
            ApiClient.CurrentUserId = user.Id;
            base.CurrentUser = RestSelfUser.Create(this, user);
        }
        
        public Task<IReadOnlyCollection<RestUser>> GetUsersAsync(params ulong[] userIds)
            => GetUsersAsync(userIds, null);
        public Task<IReadOnlyCollection<RestUser>> GetUsersAsync(ulong[] userIds, RequestOptions options = null)
            => ClientHelper.GetUsersAsync(this, userIds, options);
        public Task<IReadOnlyCollection<RestUser>> GetUsersAsync(params string[] userNames)
            => GetUsersAsync(userNames, null);
        public Task<IReadOnlyCollection<RestUser>> GetUsersAsync(string[] userNames, RequestOptions options = null)
            => ClientHelper.GetUsersAsync(this, userNames, options);
    }
}
