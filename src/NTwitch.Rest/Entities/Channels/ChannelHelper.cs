using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    internal static class ChannelHelper
    {
        public static Task ModifyAsync(RestSimpleChannel restSimpleChannel, Action<ModifyChannelParams> changes, RequestOptions options)
        {
            throw new NotImplementedException();
        }

        public static Task<RestChatBadges> GetChatBadgesAsync(BaseTwitchClient client, ulong id, RequestOptions options)
        {
            throw new NotImplementedException();
        }

        public static Task<IReadOnlyCollection<RestSimpleTeam>> GetTeamsAsync(BaseTwitchClient client, ulong id, RequestOptions options)
        {
            throw new NotImplementedException();
        }

        public static Task<IReadOnlyCollection<RestUserFollow>> GetFollowersAsync(BaseTwitchClient client, ulong id, bool ascending, uint limit, uint offset, RequestOptions options)
        {
            throw new NotImplementedException();
        }

        public static Task<IReadOnlyCollection<RestUser>> GetEditorsAsync(BaseTwitchClient client, ulong id, RequestOptions options)
        {
            throw new NotImplementedException();
        }

        public static Task<IReadOnlyCollection<RestUserSubscription>> GetSubscribersAsync(BaseTwitchClient client, ulong id, bool ascending, uint limit, uint offset, RequestOptions options)
        {
            throw new NotImplementedException();
        }

        public static Task<RestUserSubscription> GetSubscriberAsync(BaseTwitchClient client, ulong id, ulong userId, RequestOptions options)
        {
            throw new NotImplementedException();
        }

        public static Task<IReadOnlyCollection<RestVideo>> GetVideosAsync(BaseTwitchClient client, ulong id, uint limit, uint offset, RequestOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
