using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    internal static class ClientHelper
    {
        public static async Task<RestUser> GetUserAsync(BaseRestClient client, ulong id)
        {
            var model = await client.RestClient.GetUserAsync(id);
            var user = new RestUser(client, model.Id);
            user.Update(model);
            return user;
        }

        public static async Task<IEnumerable<RestUser>> GetUsersAsync(BaseRestClient client, string[] usernames)
        {
            var model = await client.RestClient.GetUsersAsync(usernames);
            var users = model.Users.Select(x =>
            {
                var user = new RestUser(client, x.Id);
                user.Update(x);
                return user;
            });
            return users;
        }

        public static async Task<IEnumerable<RestCheerInfo>> GetCheersAsync(BaseRestClient client, ulong? channelId)
        {
            var model = await client.RestClient.GetCheersAsync(channelId);
            var cheers = model.Actions.Select(x => new RestCheerInfo(client, x));
            return cheers;
        }
    }
}
