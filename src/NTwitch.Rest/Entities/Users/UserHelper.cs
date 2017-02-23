using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    internal static class UserHelper
    {
        // User

        internal static async Task<IEnumerable<RestChannelFollow>> GetFollowsAsync(IUser user, BaseRestClient client, SortMode mode, bool ascending, PageOptions options)
        {
            var request = new RequestOptions();
            request.Parameters.Add("limit", options?.Limit);
            request.Parameters.Add("offset", options?.Offset);
            request.Parameters.Add("direction",  ascending ? "asc" : "desc");
            request.Parameters.Add("sortby", mode.GetJsonValue());

            string json = await client.ApiClient.SendAsync("GET", $"users/{user.Id}/follows/channels", request).ConfigureAwait(false);
            var items = JsonConvert.DeserializeObject<IEnumerable<string>>(json, new TwitchCollectionConverter(client, "follows"));
            return items.Select(x => RestChannelFollow.Create(client, x));
        }

        internal static async Task<RestChannelFollow> GetFollowAsync(IUser user, BaseRestClient client, ulong channelId)
        {
            var request = new RequestOptions();

            string json = await client.ApiClient.SendAsync("GET", $"users/{user.Id}/follows/channels/{channelId}", request).ConfigureAwait(false);
            return RestChannelFollow.Create(client, json);
        }

        // ISelfUser

        internal static async Task<RestBlockedUser> BlockAsync(IUser user, BaseRestClient client, ulong userId)
        {
            var request = new RequestOptions();

            string json = await client.ApiClient.SendAsync("PUT", $"users/{user.Id}/blocks/{userId}", request).ConfigureAwait(false);
            return RestBlockedUser.Create(client, json);
        }

        internal static async Task UnblockAsync(IUser user, BaseRestClient client, ulong userId)
        {
            var request = new RequestOptions();

            await client.ApiClient.SendAsync("DELETE", $"users/{user.Id}/blocks/{userId}", request).ConfigureAwait(false);
        }

        public static async Task<IEnumerable<RestBlockedUser>> GetBlockedUsersAsync(ISelfUser user, BaseRestClient client, PageOptions options)
        {
            var request = new RequestOptions();
            request.Parameters.Add("limit", options?.Limit);
            request.Parameters.Add("offset", options?.Offset);

            string json = await client.ApiClient.SendAsync("GET", $"users/{user.Id}/blocks", request).ConfigureAwait(false);
            var items = JsonConvert.DeserializeObject<IEnumerable<string>>(json, new TwitchCollectionConverter(client, "blocks"));
            return items.Select(x => RestBlockedUser.Create(client, x));
        }

        public static async Task<RestChannelFollow> FollowAsync(ISelfUser user, BaseRestClient client, ulong channelId, bool notify)
        {
            var request = new RequestOptions();
            request.Parameters.Add("notifications", notify);

            string json = await client.ApiClient.SendAsync("PUT", $"users/{user.Id}/follows/channels/{channelId}", request).ConfigureAwait(false);
            return RestChannelFollow.Create(client, json);
        }

        public static Task<IEnumerable<RestStream>> GetFollowedStreamsAsync(ISelfUser user, BaseRestClient client, StreamType type, PageOptions options)
        {
            throw new NotImplementedException();
        }

        public static async Task<RestChannelSubscription> GetSubscriptionAsync(ISelfUser user, BaseRestClient client, ulong channelId)
        {
            var request = new RequestOptions();

            string json = await client.ApiClient.SendAsync("GET", $"users/{user.Id}/subscriptions/{channelId}", request).ConfigureAwait(false);
            return RestChannelSubscription.Create(client, json);
        }

        public static async Task UnfollowAsync(ISelfUser user, BaseRestClient client, ulong channelId)
        {
            var request = new RequestOptions();

            await client.ApiClient.SendAsync("DELETE", $"users/{user.Id}/follows/channels/{channelId}", request).ConfigureAwait(false);
        }

        public static Task<IEnumerable<RestVideo>> GetFollowedVideoAsync(ISelfUser user, BaseRestClient client, BroadcastType type, PageOptions options)
        {
            throw new NotImplementedException();
        }
        
        internal static Task<IEnumerable<RestChannelSubscription>> GetSubscriptionsAsync(ISelfUser user, BaseRestClient client)
        {
            throw new NotImplementedException();
        }
    }
}
