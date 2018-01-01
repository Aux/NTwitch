using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch.Helix.Rest
{
    public static class ClientHelper
    {
        public static async Task<IReadOnlyCollection<RestUser>> GetUsersAsync(BaseTwitchClient client, ulong[] userIds, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetUsersAsync(userIds, options).ConfigureAwait(false);
            return model.Select(x => RestUser.Create(client, x)).ToImmutableArray();
        }
        public static async Task<IReadOnlyCollection<RestUser>> GetUsersAsync(BaseTwitchClient client, string[] userNames, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetUsersAsync(userNames, options).ConfigureAwait(false);
            return model.Select(x => RestUser.Create(client, x)).ToImmutableArray();
        }
    }
}
