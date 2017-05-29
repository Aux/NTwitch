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
            if (!TokenHelper.TryGetToken(channel.Client, channel.Id, out RestTokenInfo info))
                throw new MissingScopeException("channel_editor");
            if (!info.Authorization.Scopes.Contains("channel_editor"))
                throw new MissingScopeException("channel_editor");

            var changes = new ModifyChannel();
            options.Invoke(changes.Parameters);

            var model = await channel.Client.RestClient.ModifyChannelInternalAsync(info.Token, channel.Id, changes);
            channel.Update(model);

            if (channel is RestChannel rc)
                await rc.UpdateAsync();
            if (channel is RestSelfChannel rsc)
                await rsc.UpdateAsync();
        }

        #endregion
        #region Users

        internal static async Task<IReadOnlyCollection<RestUser>> GetEditorsAsync(BaseRestClient client, ulong channelId)
        {
            if (!TokenHelper.TryGetToken(client, channelId, out RestTokenInfo info))
                throw new MissingScopeException("channel_read");
            if (!info.Authorization.Scopes.Contains("channel_read"))
                throw new MissingScopeException("channel_read");

            var model = await client.RestClient.GetChannelEditorsAsync(info.Token, channelId);

            var entity = model.Users.Select(x => RestUser.Create(client, x));
            return entity.ToArray();
        }

        internal static async Task<IReadOnlyCollection<RestSimpleTeam>> GetTeamsAsync(BaseRestClient client, ulong channelId)
        {
            TokenHelper.TryGetToken(client, channelId, out RestTokenInfo info);
            var model = await client.RestClient.GetChannelTeamsInternalAsync(info?.Token, channelId);

            var entity = model.Teams.Select(x => RestSimpleTeam.Create(client, x));
            return entity.ToArray();
        }

        internal static async Task<IReadOnlyCollection<RestUserFollow>> GetFollowersAsync(BaseRestClient client, ulong channelId, bool ascending, uint limit, uint offset)
        {
            TokenHelper.TryGetToken(client, channelId, out RestTokenInfo info);
            var model = await client.RestClient.GetChannelFollowersInternalAsync(info?.Token, channelId, ascending, limit, offset);

            var entity = model.Follows.Select(x => RestUserFollow.Create(client, x));
            return entity.ToArray();
        }

        internal static async Task<IReadOnlyCollection<RestUserSubscription>> GetSubscribersAsync(BaseRestClient client, ulong channelId, bool ascending, uint limit, uint offset)
        {
            if (!TokenHelper.TryGetToken(client, channelId, out RestTokenInfo info))
                throw new MissingScopeException("channel_subscriptions");
            if (info.Authorization.Scopes.Contains("channel_subscriptions"))
                throw new MissingScopeException("channel_subscriptions");

            var model = await client.RestClient.GetChannelSubscribersInternalAsync(info.Token, channelId, ascending, limit, offset);

            var entity = model.Subscriptions.Select(x => RestUserSubscription.Create(client, x));
            return entity.ToArray();
        }

        internal static async Task<RestUserSubscription> GetSubscriberAsync(BaseRestClient client, ulong channelId, ulong userId)
        {
            if (!TokenHelper.TryGetToken(client, channelId, out RestTokenInfo info))
                throw new MissingScopeException("channel_subscriptions");
            if (info.Authorization.Scopes.Contains("channel_subscriptions"))
                throw new MissingScopeException("channel_subscriptions");

            var model = await client.RestClient.GetSubscriberInternalAsync(info.Token, channelId, userId);
            if (model == null)
                return null;

            return RestUserSubscription.Create(client, model);
        }

        #endregion
        #region Chat

        internal static async Task<RestChatBadges> GetChatBadgesAsync(BaseRestClient client, ulong channelId)
        {
            TokenHelper.TryGetToken(client, channelId, out RestTokenInfo info);
            var model = await client.RestClient.GetBadgesInternalAsync(info?.Token, channelId);
            if (model == null)
                return null;

            return RestChatBadges.Create(model);
        }

        #endregion
    }
}
