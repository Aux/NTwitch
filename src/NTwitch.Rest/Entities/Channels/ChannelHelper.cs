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

        public static Task<RestChatBadges> GetChatBadgesAsync(TwitchRestClient client, ulong id, RequestOptions options)
        {
            throw new NotImplementedException();
        }

        public static Task<IReadOnlyCollection<RestSimpleTeam>> GetTeamsAsync(TwitchRestClient client, ulong id, RequestOptions options)
        {
            throw new NotImplementedException();
        }

        public static Task<IReadOnlyCollection<RestUserFollow>> GetFollowersAsync(TwitchRestClient client, ulong id, bool ascending, uint limit, uint offset, RequestOptions options)
        {
            throw new NotImplementedException();
        }

        public static Task<IReadOnlyCollection<RestUser>> GetEditorsAsync(TwitchRestClient client, ulong id, RequestOptions options)
        {
            throw new NotImplementedException();
        }

        public static Task<IReadOnlyCollection<RestUserSubscription>> GetSubscribersAsync(TwitchRestClient client, ulong id, bool ascending, uint limit, uint offset, RequestOptions options)
        {
            throw new NotImplementedException();
        }

        public static Task<RestUserSubscription> GetSubscriberAsync(TwitchRestClient client, ulong id, ulong userId, RequestOptions options)
        {
            throw new NotImplementedException();
        }

        public static Task<IReadOnlyCollection<RestVideo>> GetVideosAsync(TwitchRestClient client, ulong id, uint limit, uint offset, RequestOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
