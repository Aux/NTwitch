using System;
using System.Collections.Generic;
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
        public static Task<RestChatBadges> GetChatBadgesAsync(BaseTwitchClient client, ulong id, RequestOptions options = null)
        {
            throw new NotImplementedException();
        }

        // Teams
        public static Task<IReadOnlyCollection<RestSimpleTeam>> GetTeamsAsync(BaseTwitchClient client, ulong id, RequestOptions options = null)
        {
            throw new NotImplementedException();
        }

        // Users
        public static Task<IReadOnlyCollection<RestUserFollow>> GetFollowersAsync(BaseTwitchClient client, ulong id, bool ascending, PageOptions paging = null, RequestOptions options = null)
        {
            throw new NotImplementedException();
        }

        public static Task<IReadOnlyCollection<RestUser>> GetEditorsAsync(BaseTwitchClient client, ulong id, RequestOptions options = null)
        {
            throw new NotImplementedException();
        }

        public static Task<IReadOnlyCollection<RestUserSubscription>> GetSubscribersAsync(BaseTwitchClient client, ulong id, bool ascending, PageOptions paging = null, RequestOptions options = null)
        {
            throw new NotImplementedException();
        }

        public static Task<RestUserSubscription> GetSubscriberAsync(BaseTwitchClient client, ulong id, ulong userId, RequestOptions options = null)
        {
            throw new NotImplementedException();
        }

        // Videos
        public static Task<IReadOnlyCollection<RestVideo>> GetVideosAsync(BaseTwitchClient client, ulong id, PageOptions paging = null, RequestOptions options = null)
        {
            throw new NotImplementedException();
        }
    }
}
