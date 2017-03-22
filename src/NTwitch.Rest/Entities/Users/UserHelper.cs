using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    internal static class UserHelper
    {
        internal static async Task<RestChannelFollow> GetFollowAsync(RestSimpleUser user, ulong channelId)
        {
            var model = await user.Client.RestClient.GetFollowAsync(user.Id, channelId);
            if (model == null) return null;

            var entity = new RestChannelFollow(user.Client);
            entity.Update(model);
            return entity;
        }

        internal static async Task<IEnumerable<RestChannelFollow>> GetFollowsAsync(RestSimpleUser user, SortMode sort, bool ascending, uint limit, uint offset)
        {
            var model = await user.Client.RestClient.GetFollowsAsync(user.Id, sort, ascending, limit, offset);
            var entity = model.Follows.Select(x =>
            {
                var follow = new RestChannelFollow(user.Client);
                follow.Update(x);
                return follow;
            });
            return entity;
        }

        internal static async Task<IReadOnlyDictionary<string, IEnumerable<RestEmote>>> GetEmotesAsync(RestSimpleUser user, ulong id)
        {
            var model = await user.Client.RestClient.GetEmotesAsync(user.Id);
            var entity = model.Emotes.Select(x =>
            {
                var values = x.Value.Select(y =>
                {
                    var emote = new RestEmote(user.Client, y.Id);
                    emote.Update(y);
                    return emote;
                });
                return new KeyValuePair<string, IEnumerable<RestEmote>>(x.Key, values);
            });
            return entity.ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
