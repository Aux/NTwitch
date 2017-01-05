using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
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

        public static async Task<RestUser> GetUserAsync(TwitchRestClient client, ulong id)
        {
            var json = await client.ApiClient.GetJsonAsync("GET", "users/" + id);
            return RestUser.Create(client, json);
        }

        public static async Task<IEnumerable<RestUser>> FindUsersAsync(TwitchRestClient client, string login)
        {
            var json = await client.ApiClient.GetJsonAsync("GET", "users?login=" + login);
            var items = JsonConvert.DeserializeObject<IEnumerable<string>>(json, new TwitchCollectionConverter("users"));
            return items.Select(x => RestUser.Create(client, x));
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

        public static async Task<IEnumerable<RestChannel>> FindChannelsAsync(TwitchRestClient client, string query, TwitchPageOptions options)
        {
            var json = await client.ApiClient.GetJsonAsync("GET", "search/channels?query=" + query);
            var items = JsonConvert.DeserializeObject<IEnumerable<string>>(json, new TwitchCollectionConverter("channels"));
            return items.Select(x => RestChannel.Create(client, x));
        }
    }
}
