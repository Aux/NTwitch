using System.Threading.Tasks;

namespace NTwitch.Rest
{
    internal static class ClientHelper
    {
        public static async Task<RestSelfUser> GetCurrentUserAsync(TwitchRestClient client)
        {
            var json = await client.ApiClient.GetJsonAsync("GET", "user");
            return RestSelfUser.Create(client, json);
        }

        public static async Task<RestSelfChannel> GetCurrentChannelAsync(TwitchRestClient client)
        {
            var json = await client.ApiClient.GetJsonAsync("GET", "channels");
            return RestSelfChannel.Create(client, json);
        }

        public static async Task<RestChannel> GetChannelAsync(TwitchRestClient client, ulong id)
        {
            var json = await client.ApiClient.GetJsonAsync("GET", "channels/" + id);
            return RestChannel.Create(client, json);
        }

        public static async Task<RestUser> GetUserAsync(TwitchRestClient client, ulong id)
        {
            var json = await client.ApiClient.GetJsonAsync("GET", "users/" + id);
            return RestUser.Create(client, json);
        }
    }
}
