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
        // Users
        public static async Task<IReadOnlyCollection<RestUser>> GetUsersAsync(BaseTwitchClient client, ulong[] userIds, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetUsersAsync(userIds, options).ConfigureAwait(false);
            return model.Select(x => RestUser.Create(client, x)).ToImmutableArray();
        }
        public static async Task<IReadOnlyCollection<RestUser>> GetUsersAsync(BaseTwitchClient client, string[] userNames, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetUsersAsync(userNames, options).ConfigureAwait(false);
            return model.Select(x => RestUser.Create(client, x)).ToImmutableArray();
        }

        // Followers
        public static IAsyncEnumerable<IReadOnlyCollection<RestFollower>> GetFollowersAsync(BaseTwitchClient client, ulong? userId, ulong? followerId, int limit = 20, RequestOptions options = null)
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
    }
}
