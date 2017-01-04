using Newtonsoft.Json;
using System.Threading.Tasks;
using System;

namespace NTwitch.Rest
{
    internal static class ClientHelper
    {
        public static async Task<RestSelfUser> GetCurrentUserAsync(ITwitchClient client)
        {
            var user = new RestSelfUser(client);
            var json = await user.Client.ApiClient.GetJsonAsync("GET", "/user");
            JsonConvert.PopulateObject(json, user);
            return user;
        }

        public static async Task<RestSelfChannel> GetCurrentChannelAsync(ITwitchClient client)
        {
            var channel = new RestSelfChannel(client);
            var json = await channel.Client.ApiClient.GetJsonAsync("GET", "/channels");
            JsonConvert.PopulateObject(json, channel);
            return channel;
        }

        public static async Task<RestChannel> GetChannelAsync(ITwitchClient client, ulong id)
        {
            var channel = new RestChannel(client);
            var json = await channel.Client.ApiClient.GetJsonAsync("GET", "/channels/" + id);
            JsonConvert.PopulateObject(json, channel);
            return channel;
        }

        public static async Task<RestUser> GetUserAsync(ITwitchClient client, ulong id)
        {
            var user = new RestUser(client);
            var json = await user.Client.ApiClient.GetJsonAsync("GET", "/users/" + id);
            JsonConvert.PopulateObject(json, user);
            return user;
        }

        public static async Task<RestUser> GetUserAsync(ITwitchClient client, string name)
        {
            var user = new RestUser(client);
            var json = await user.Client.ApiClient.GetJsonAsync("GET", "/users?login=" + name);
            JsonConvert.PopulateObject(json, user);
            return user;
        }
    }
}
