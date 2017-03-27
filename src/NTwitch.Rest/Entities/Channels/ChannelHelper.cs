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
            if (!channel.Client.Token.Authorization.Scopes.Contains("channel_editor"))
                throw new MissingScopeException("channel_editor");

            var model = await channel.Client.RestClient.ModifyChannelAsync(channel.Id, options);
            channel.Update(model);
        }

        #endregion
        #region Users

        internal static async Task<IReadOnlyCollection<RestUser>> GetEditorsAsync(RestSimpleChannel channel)
        {
            if (!channel.Client.Token.Authorization.Scopes.Contains("channel_read"))
                throw new MissingScopeException("channel_read");

            var model = await channel.Client.RestClient.GetChannelEditorsAsync(channel.Id);

            var entity = model.Users.Select(x =>
            {
                var user = new RestUser(channel.Client, x.Id);
                user.Update(x);
                return user;
            });
            return entity.ToArray();
        }

        internal static async Task<IReadOnlyCollection<RestSimpleTeam>> GetTeamsAsync(RestSimpleChannel channel)
        {
            var model = await channel.Client.RestClient.GetChannelTeamsAsync(channel.Id);

            var entity = model.Teams.Select(x =>
            {
                var team = new RestSimpleTeam(channel.Client, x.Id);
                team.Update(x);
                return team;
            });
            return entity.ToArray();
        }

        internal static async Task<IReadOnlyCollection<RestUserFollow>> GetFollowersAsync(RestSimpleChannel channel, bool ascending, uint limit, uint offset)
        {
            var model = await channel.Client.RestClient.GetChannelFollowsAsync(channel.Id, ascending, limit, offset);

            var entity = model.Follows.Select(x =>
            {
                var follow = new RestUserFollow(channel.Client);
                follow.Update(x);
                return follow;
            });
            return entity.ToArray();
        }

        internal static async Task<IReadOnlyCollection<RestUserSubscription>> GetSubscribersAsync(RestSimpleChannel channel, bool ascending, uint limit, uint offset)
        {
            if (!channel.Client.Token.Authorization.Scopes.Contains("channel_subscriptions"))
                throw new MissingScopeException("channel_subscriptions");

            var model = await channel.Client.RestClient.GetChannelSubscriptionsAsync(channel.Id, ascending, limit, offset);

            var entity = model.Subscriptions.Select(x =>
            {
                var sub = new RestUserSubscription(channel.Client);
                sub.Update(x);
                return sub;
            });
            return entity.ToArray();
        }

        internal static async Task<RestUserSubscription> GetSubscriberAsync(RestSimpleChannel channel, ulong userId)
        {
            var model = await channel.Client.RestClient.GetSubscriberAsync(channel.Id, userId);
            if (model == null)
                return null;

            var entity = new RestUserSubscription(channel.Client);
            entity.Update(model);
            return entity;
        }

        #endregion
        #region Chat

        internal static async Task<RestChatBadges> GetChatBadgesAsync(RestSimpleChannel channel)
        {
            var model = await channel.Client.RestClient.GetChatBadgesAsync(channel.Id);
            if (model == null)
                return null;

            var entity = new RestChatBadges();
            entity.Update(model);
            return entity;
        }

        #endregion
    }
}
