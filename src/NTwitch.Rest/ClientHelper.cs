using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    internal static class ClientHelper
    {
        //
        //  Users
        //
        
        public static async Task<RestSelfUser> GetCurrentUserAsync(BaseRestClient client)
        {
            if (!client.Token.IsValid)
                throw new NotSupportedException("You must log in with oauth to get the current user.");
            if (!client.Token.Authorization.Scopes.Contains("user_read"))
                throw new MissingScopeException("user_read");

            var model = await client.RestClient.GetCurrentUserAsync();
            var user = new RestSelfUser(client, model.Id);
            user.Update(model);
            return user;
        }

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

        //
        //  Channels
        //

        public static async Task<RestSelfChannel> GetCurrentChannelAsync(BaseRestClient client)
        {
            if (!client.Token.IsValid)
                throw new NotSupportedException("You must log in with oauth to get the current channel.");
            if (!client.Token.Authorization.Scopes.Contains("channel_read"))
                throw new MissingScopeException("channel_read");

            var model = await client.RestClient.GetCurrentChannelAsync();
            var channel = new RestSelfChannel(client, model.Id);
            channel.Update(model);
            return channel;
        }

        public static async Task<RestChannel> GetChannelAsync(BaseRestClient client, ulong channelId)
        {
            var model = await client.RestClient.GetChannelAsync(channelId);
            var channel = new RestChannel(client, model.Id);
            channel.Update(model);
            return channel;
        }

        public static async Task<IEnumerable<RestCheerInfo>> GetCheersAsync(BaseRestClient client, ulong? channelId)
        {
            var model = await client.RestClient.GetCheersAsync(channelId);
            var cheers = model.Actions.Select(x => new RestCheerInfo(client, x));
            return cheers;
        }
    }
}
