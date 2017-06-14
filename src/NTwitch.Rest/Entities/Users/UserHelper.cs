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
        public static Task<IReadOnlyCollection<RestChannelFollow>> GetFollowsAsync(RestSimpleUser restSimpleUser, RequestOptions options)
        {
            throw new NotImplementedException();
        }

        public static Task<RestChannelFollow> GetFollowAsync(RestSimpleUser restSimpleUser, ulong channelId, RequestOptions options)
        {
            throw new NotImplementedException();
        }

        // Heartbeat
        public static Task<string> CreateHeartbeatAsync(RestSimpleUser restSimpleUser, RequestOptions options)
        {
            throw new NotImplementedException();
        }

        public static Task<string> GetHeartbeatAsync(RestSimpleUser restSimpleUser, RequestOptions options)
        {
            throw new NotImplementedException();
        }

        public static Task DeleteHeartbeatAsync(RestSimpleUser restSimpleUser, RequestOptions options)
        {
            throw new NotImplementedException();
        }

        // Subscriptions
        public static Task<RestChannelSubscription> GetSubscrptionAsync(RestSimpleUser restSimpleUser, RequestOptions options)
        {
            throw new NotImplementedException();
        }

        // Users
        public static Task BlockAsync(RestSimpleUser restSimpleUser, RequestOptions options)
        {
            throw new NotImplementedException();
        }

        public static Task UnblockAsync(RestSimpleUser restSimpleUser, RequestOptions options)
        {
            throw new NotImplementedException();
        }

        public static Task<IReadOnlyCollection<RestBlockedUser>> GetBlocksAsync(RestSimpleUser restSimpleUser, PageOptions paging, RequestOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
