using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    internal static class ChannelHelper
    {
        // Channel
        public static async Task ModifyAsync(RestSimpleChannel channel, Action<ModifyChannelParams> changes, RequestOptions options = null)
        {
            var filledParams = new ModifyChannelParams();
            changes.Invoke(filledParams);
            
            var model = await channel.Client.ApiClient.ModifyChannelAsync(channel.Id, filledParams, options).ConfigureAwait(false);
            if (model != null)
                channel.Update(model);
        }

        // Chat
        public static async Task<RestChatBadges> GetChatBadgesAsync(BaseTwitchClient client, ulong channelId, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetChatBadgesAsync(channelId, options).ConfigureAwait(false);
            return RestChatBadges.Create(model);
        }

        // Teams
        public static async Task<IReadOnlyCollection<RestSimpleTeam>> GetTeamsAsync(BaseTwitchClient client, ulong channelId, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetChannelTeamsAsync(channelId, options).ConfigureAwait(false);
            return model.Teams.Select(x => RestSimpleTeam.Create(client, x)).ToArray();
        }

        // Users
        public static async Task<IReadOnlyCollection<RestUserFollow>> GetFollowersAsync(BaseTwitchClient client, ulong channelId, bool ascending, PageOptions paging = null, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetChannelFollowersAsync(channelId, ascending, paging, options).ConfigureAwait(false);
            return model.Follows.Select(x => RestUserFollow.Create(client, x)).ToArray();
        }

        public static async Task<IReadOnlyCollection<RestUser>> GetEditorsAsync(BaseTwitchClient client, ulong channelId, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetChannelEditorsAsync(channelId, options).ConfigureAwait(false);
            return model.Users.Select(x => RestUser.Create(client, x)).ToArray();
        }

        public static async Task<IReadOnlyCollection<RestUserSubscription>> GetSubscribersAsync(BaseTwitchClient client, ulong channelId, bool ascending, PageOptions paging = null, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetChannelSubscribersAsync(channelId, ascending, paging, options).ConfigureAwait(false);
            return model.Subscriptions.Select(x => RestUserSubscription.Create(client, x)).ToArray();
        }

        public static async Task<RestUserSubscription> GetSubscriberAsync(BaseTwitchClient client, ulong channelId, ulong userId, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetChannelSubscriberAsync(channelId, userId, options).ConfigureAwait(false);
            return RestUserSubscription.Create(client, model);
        }

        // Videos
        public static async Task<IReadOnlyCollection<RestVideo>> GetVideosAsync(BaseTwitchClient client, ulong channelId, PageOptions paging = null, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetChannelVideosAsync(channelId, paging, options).ConfigureAwait(false);
            return model.Videos.Select(x => RestVideo.Create(client, x)).ToArray();
        }
    }
}
