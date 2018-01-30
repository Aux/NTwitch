using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using NTwitch.Helix.API;
using NTwitch.Utility.Paging;

namespace NTwitch.Helix.Rest
{
    internal static class ClientHelper
    {
        // Clips
        public static async Task<RestSimpleClip> CreateClipAsync(BaseTwitchClient client, ulong broadcastId, RequestOptions options = null)
        {
            var model = await client.ApiClient.CreateClipAsync(broadcastId, options).ConfigureAwait(false);
            return RestSimpleClip.Create(client, model);
        }
        public static async Task<RestClip> GetClipAsync(BaseTwitchClient client, string clipId, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetClipAsync(clipId, options).ConfigureAwait(false);
            if (model != null)
                return RestClip.Create(client, model);
            return null;
        }

        // Games
        public static async Task<IReadOnlyCollection<RestGame>> GetGamesAsync(BaseTwitchClient client, ulong[] gameIds = null, string[] gameNames = null, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetGamesAsync(gameIds, gameNames, options).ConfigureAwait(false);
            return model.Select(x => RestGame.Create(client, x)).ToImmutableArray();
        }
        public static async Task<IReadOnlyCollection<RestGame>> GetTopGamesAsync(BaseTwitchClient client, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetTopGamesAsync(options).ConfigureAwait(false);
            return model.Select(x => RestGame.Create(client, x)).ToImmutableArray();
        }

        // Broadcasts
        public static IAsyncEnumerable<IReadOnlyCollection<RestBroadcast>> GetBroadcastsAsync(BaseTwitchClient client, string[] communityIds = null, string[] languages = null, ulong[] userIds = null, string[] userNames = null, BroadcastType? broadcastType = null, int limit = 20, RequestOptions options = null)
        {
            return new PagedAsyncEnumerable<RestBroadcast>(
                TwitchConfig.MaxBroadcastsPerBatch,
                async (info, ct) =>
                {
                    var args = new GetBroadcastsParams
                    {
                        CommunityIds = communityIds,
                        Languages = languages,
                        UserIds = userIds,
                        UserNames = userNames,
                        Type = broadcastType.GetValueOrDefault(),
                        First = info.Remaining > TwitchConfig.MaxBroadcastsPerBatch ? TwitchConfig.MaxBroadcastsPerBatch : info.Remaining.Value,
                        After = info.Cursor
                    };

                    var data = await client.ApiClient.GetBroadcastsAsync(args, options).ConfigureAwait(false);
                    info.Cursor = data.Pagination.Value.Cursor;
                    return data.Data.Select(x => RestBroadcast.Create(client, x)).ToImmutableArray();
                },
                nextPage: (info, lastPage) =>
                {
                    if (lastPage.Count != TwitchConfig.MaxBroadcastsPerBatch)
                        return false;
                    info.Position = lastPage.Max(x => x.Id);
                    return true;
                },
                count: limit
            );
        }
        public static Task GetBroadcastMetadataAsync()
        {
            throw new System.NotImplementedException();
        }
        
        // Users
        public static async Task<IReadOnlyCollection<RestUser>> GetUsersAsync(BaseTwitchClient client, ulong[] userIds = null, string[] userNames = null, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetUsersAsync(userIds, userNames, options).ConfigureAwait(false);
            return model.Select(x => RestUser.Create(client, x)).ToImmutableArray();
        }
        public static async Task<RestUser> ModifyMyUserAsync(BaseTwitchClient client, string description, RequestOptions options = null)
        {
            var model = await client.ApiClient.ModifyMyUserAsync(description, options).ConfigureAwait(false);
            return RestUser.Create(client, model);
        }

        // Followers
        public static IAsyncEnumerable<IReadOnlyCollection<RestFollower>> GetFollowersAsync(BaseTwitchClient client, ulong? userId = null, ulong? followerId = null, int limit = 20, RequestOptions options = null)
        {
            return new PagedAsyncEnumerable<RestFollower>(
                TwitchConfig.MaxFollowersPerBatch,
                async (info, ct) =>
                {
                    var args = new GetFollowersParams
                    {
                        FromId = userId.GetValueOrDefault(),
                        ToId = followerId.GetValueOrDefault(),
                        First = info.Remaining > TwitchConfig.MaxFollowersPerBatch ? TwitchConfig.MaxFollowersPerBatch : info.Remaining.Value,
                        After = info.Cursor
                    };

                    var data = await client.ApiClient.GetFollowersAsync(args, options).ConfigureAwait(false);
                    info.Cursor = data.Pagination.Value.Cursor;
                    return data.Data.Select(x => RestFollower.Create(client, x)).ToImmutableArray();
                },
                nextPage: (info, lastPage) =>
                {
                    if (lastPage.Count != TwitchConfig.MaxFollowersPerBatch)
                        return false;
                    info.Position = lastPage.Max(x => x.Id);
                    return true;
                },
                count: limit
            );
        }

        // Videos

    }
}
