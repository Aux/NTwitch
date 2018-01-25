using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

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

        // Clips
        public Task<RestClip> GetClipAsync(string clipId, RequestOptions options = null)
            => ClientHelper.GetClipAsync(this, clipId, options);

        // Games
        public async Task<RestGame> GetGameAsync(ulong gameId, RequestOptions options = null)
            => (await GetGamesAsync(gameIds: new[] { gameId }, options: options).ConfigureAwait(false)).FirstOrDefault();
        public async Task<RestGame> GetGameAsync(string gameName, RequestOptions options = null)
            => (await GetGamesAsync(gameNames: new[] { gameName }, options: options).ConfigureAwait(false)).FirstOrDefault();

        public Task<IReadOnlyCollection<RestGame>> GetGamesAsync(params ulong[] gameIds)
            => GetGamesAsync(gameIds, null, null);
        public Task<IReadOnlyCollection<RestGame>> GetGamesAsync(params string[] gameNames)
            => GetGamesAsync(null, gameNames, null);

        public Task<IReadOnlyCollection<RestGame>> GetGamesAsync(ulong[] gameIds = null, string[] gameNames = null, RequestOptions options = null)
            => ClientHelper.GetGamesAsync(this, gameIds, gameNames, options);
        public Task<IReadOnlyCollection<RestGame>> GetTopGamesAsync(RequestOptions options = null)
            => ClientHelper.GetTopGamesAsync(this, options);

        // Broadcasts
        public async Task<RestBroadcast> GetBroadcastAsync(ulong userId, RequestOptions options = null)
            => (await GetBroadcastsAsync(userIds: new[] { userId }, options: options).Flatten().ConfigureAwait(false)).FirstOrDefault();
        public async Task<RestBroadcast> GetBroadcastAsync(string userName, RequestOptions options = null)
            => (await GetBroadcastsAsync(userNames: new[] { userName }, options: options).Flatten().ConfigureAwait(false)).FirstOrDefault();
        
        public IAsyncEnumerable<IReadOnlyCollection<RestBroadcast>> GetBroadcastsAsync(string[] communityIds = null, string[] languages = null, ulong[] userIds = null, string[] userNames = null, BroadcastType? broadcastType = null, int limit = 20, RequestOptions options = null)
            => ClientHelper.GetBroadcastsAsync(this, communityIds, languages, userIds, userNames, broadcastType, limit, options);

        // Users
        public async Task<RestUser> GetUserAsync(ulong userId, RequestOptions options = null)
            => (await GetUsersAsync(userIds: new[] { userId }, options: options).ConfigureAwait(false)).FirstOrDefault();
        public async Task<RestUser> GetUserAsync(string userName, RequestOptions options = null)
            => (await GetUsersAsync(userNames: new[] { userName }, options: options).ConfigureAwait(false)).FirstOrDefault();

        public Task<IReadOnlyCollection<RestUser>> GetUsersAsync(params ulong[] userIds)
            => GetUsersAsync(userIds, null, null);
        public Task<IReadOnlyCollection<RestUser>> GetUsersAsync(params string[] userNames)
            => GetUsersAsync(null, userNames, null);
        public Task<IReadOnlyCollection<RestUser>> GetUsersAsync(ulong[] userIds = null, string[] userNames = null, RequestOptions options = null)
            => ClientHelper.GetUsersAsync(this, userIds, userNames, options);
    }
}
