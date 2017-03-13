using System.Threading.Tasks;

namespace NTwitch.Rest
{
    internal static class ClientHelper
    {
        public static async Task<RestUser> GetUserAsync(BaseRestClient client, ulong id)
        {
            var model = await client.Client.GetUserAsync(id);
            var user = new RestUser(client, model.Id);
            user.Update(model);
            return user;
        }
    }
}
