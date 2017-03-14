using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    internal static class UserHelper
    {
        internal static async Task<RestChannelFollow> GetFollowAsync(RestUser user, ulong channelId)
        {
            var model = await user.Client.RestClient.GetFollowAsync(user.Id, channelId);
            if (model == null) return null;

            var follow = new RestChannelFollow(user.Client);
            follow.Update(model);
            return follow;
        }

        internal static async Task<IEnumerable<RestChannelFollow>> GetFollowsAsync(RestUser user, SortMode sort, bool ascending, int limit, int offset)
        {
            var model = await user.Client.RestClient.GetFollowsAsync(user.Id, sort, ascending, limit, offset);
            var follows = model.Follows.Select(x =>
            {
                var follow = new RestChannelFollow(user.Client);
                follow.Update(x);
                return follow;
            });
            return follows;
        }
    }
}
