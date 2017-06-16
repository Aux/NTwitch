using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    internal static class UserHelper
    {
        // Chat
        public static async Task<IReadOnlyDictionary<string, IReadOnlyCollection<RestEmote>>> GetEmotesAsync(BaseTwitchClient client, ulong userId, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetEmotesAsync(userId, options).ConfigureAwait(false);
            var entity = model.Emotes.Select(x =>
            {
                var values = x.Value.Select(y => RestEmote.Create(client, y));
                return new KeyValuePair<string, IReadOnlyCollection<RestEmote>>(x.Key, values.ToArray());
            });
            return entity.ToDictionary(x => x.Key, x => x.Value);
        }

        // Follows
        public static async Task<IReadOnlyCollection<RestChannelFollow>> GetFollowsAsync(BaseTwitchClient client, ulong userId, SortMode sort, bool ascending, PageOptions paging = null, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetFollowsAsync(userId, sort, ascending, paging, options).ConfigureAwait(false);
            return model.Follows.Select(x => RestChannelFollow.Create(client, x)).ToArray();
        }

        public static async Task<RestChannelFollow> GetFollowAsync(BaseTwitchClient client, ulong userId, ulong channelId, RequestOptions options)
        {
            var model = await client.ApiClient.GetFollowAsync(userId, channelId, options).ConfigureAwait(false);
            return RestChannelFollow.Create(client, model);
        }

        //// Heartbeat
        //public static async Task CreateHeartbeatAsync(BaseTwitchClient client, ulong userId, RequestOptions options)
        //{
        //    var model = await client.ApiClient.CreateHeartbeatAsync(userId, options).ConfigureAwait(false);
        //}

        //public static async Task<string> GetHeartbeatAsync(BaseTwitchClient client, ulong userId, RequestOptions options)
        //{
        //    throw new NotImplementedException();
        //}

        //public static async Task DeleteHeartbeatAsync(BaseTwitchClient client, ulong userId, RequestOptions options)
        //{
        //    throw new NotImplementedException();
        //}

        // Subscriptions
        public static async Task<RestChannelSubscription> GetSubscrptionAsync(BaseTwitchClient client, ulong userId, ulong channelId, RequestOptions options)
        {
            var model = await client.ApiClient.GetUserSubscriptionAsync(userId, channelId, options).ConfigureAwait(false);
            return RestChannelSubscription.Create(client, model);
        }

        // Users
        public static async Task BlockAsync(BaseTwitchClient client, ulong userId, ulong victimId, RequestOptions options)
        {
            await client.ApiClient.BlockUserAsync(userId, victimId, options).ConfigureAwait(false);
        }

        public static async Task UnblockAsync(BaseTwitchClient client, ulong userId, ulong victimId, RequestOptions options)
        {
            await client.ApiClient.UnblockUserAsync(userId, victimId, options).ConfigureAwait(false);
        }

        public static async Task<IReadOnlyCollection<RestBlockedUser>> GetBlocksAsync(BaseTwitchClient client, ulong userId, PageOptions paging, RequestOptions options)
        {
            var model = await client.ApiClient.GetUserBlocksAsync(userId, paging, options).ConfigureAwait(false);
            return model.Blocks.Select(x => RestBlockedUser.Create(client, x)).ToArray();

        }
    }
}
