using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    internal static class ChannelHelper
    {
        #region Channel

        public static async Task ModifyChannelAsync(RestSimpleChannel channel, Action<ModifyChannelParams> options)
        {
            if (!channel.Client.Token.Authorization?.Scopes.Contains("channel_editor") ?? false)
                throw new MissingScopeException("channel_editor");

            var model = await channel.Client.RestClient.ModifyChannelAsync(channel.Id, options);
            channel.Update(model);
        }

        #endregion
        #region Users

        internal static async Task<IReadOnlyCollection<RestUser>> GetEditorsAsync(BaseRestClient client, ulong channelId)
        {
            if (!client.Token.Authorization?.Scopes.Contains("channel_read") ?? false)
                throw new MissingScopeException("channel_read");

            var model = await client.RestClient.GetChannelEditorsAsync(channelId);

            var entity = model.Users.Select(x =>
            {
                var user = new RestUser(client, x.Id);
                user.Update(x);
                return user;
            });
            return entity.ToArray();
        }

        internal static async Task<IReadOnlyCollection<RestSimpleTeam>> GetTeamsAsync(BaseRestClient client, ulong channelId)
        {
            var model = await client.RestClient.GetChannelTeamsAsync(channelId);

            var entity = model.Teams.Select(x =>
            {
                var team = new RestSimpleTeam(client, x.Id);
                team.Update(x);
                return team;
            });
            return entity.ToArray();
        }

        internal static async Task<IReadOnlyCollection<RestUserFollow>> GetFollowersAsync(BaseRestClient client, ulong channelId, bool ascending, uint limit, uint offset)
        {
            var model = await client.RestClient.GetChannelFollowsAsync(channelId, ascending, limit, offset);

            var entity = model.Follows.Select(x =>
            {
                var follow = new RestUserFollow(client);
                follow.Update(x);
                return follow;
            });
            return entity.ToArray();
        }

        internal static async Task<IReadOnlyCollection<RestUserSubscription>> GetSubscribersAsync(BaseRestClient client, ulong channelId, bool ascending, uint limit, uint offset)
        {
            if (!client.Token.Authorization?.Scopes.Contains("channel_subscriptions") ?? false)
                throw new MissingScopeException("channel_subscriptions");

            var model = await client.RestClient.GetChannelSubscriptionsAsync(channelId, ascending, limit, offset);

            var entity = model.Subscriptions.Select(x =>
            {
                var sub = new RestUserSubscription(client);
                sub.Update(x);
                return sub;
            });
            return entity.ToArray();
        }

        internal static async Task<RestUserSubscription> GetSubscriberAsync(BaseRestClient client, ulong channelId, ulong userId)
        {
            var model = await client.RestClient.GetSubscriberAsync(channelId, userId);
            if (model == null)
                return null;

            var entity = new RestUserSubscription(client);
            entity.Update(model);
            return entity;
        }

        #endregion
        #region Chat

        internal static async Task<RestChatBadges> GetChatBadgesAsync(BaseRestClient client, ulong channelId)
        {
            var model = await client.RestClient.GetChatBadgesAsync(channelId);
            if (model == null)
                return null;

            var entity = new RestChatBadges();
            entity.Update(model);
            return entity;
        }

        #endregion
    }
}
