using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    internal static class ClientHelper
    {
        public static async Task<RestTokenInfo> AuthorizeAsync(BaseRestClient client)
        {
            await client.Logger.InfoAsync("Rest", "Logging in...").ConfigureAwait(false);
            
            var model = await client.RestClient.ValidateTokenAsync();
            var entity = RestTokenInfo.Create(model);
            
            await client.Logger.InfoAsync("Rest", "Login success!").ConfigureAwait(false);
            return entity;
        }

        internal static async Task<IEnumerable<RestIngest>> GetIngestsAsync(BaseRestClient client)
        {
            var model = await client.RestClient.GetIngestsAsync();

            var entity = model.Ingests.Select(x =>
            {
                var ingest = new RestIngest(client, x.Id);
                ingest.Update(x);
                return ingest;
            });
            return entity;
        }

        #region Users

        public static async Task<RestSelfUser> GetCurrentUserAsync(BaseRestClient client)
        {
            if (!client.Token.IsValid)
                throw new NotSupportedException("You must log in with oauth to get the current user.");
            if (!client.Token.Authorization.Scopes.Contains("user_read"))
                throw new MissingScopeException("user_read");

            var model = await client.RestClient.GetCurrentUserAsync();
            var entity = new RestSelfUser(client, model.Id);
            entity.Update(model);
            return entity;
        }

        public static async Task<RestUser> GetUserAsync(BaseRestClient client, ulong id)
        {
            var model = await client.RestClient.GetUserAsync(id);
            if (model == null)
                return null;

            var entity = new RestUser(client, model.Id);
            entity.Update(model);
            return entity;
        }
        
        public static async Task<IEnumerable<RestUser>> GetUsersAsync(BaseRestClient client, string[] usernames)
        {
            var model = await client.RestClient.GetUsersAsync(usernames);
            if (model == null)
                return new List<RestUser>();

            var entity = model.Users.Select(x =>
            {
                var user = new RestUser(client, x.Id);
                user.Update(x);
                return user;
            });
            return entity;
        }

        #endregion
        #region Channels

        public static async Task<RestSelfChannel> GetCurrentChannelAsync(BaseRestClient client)
        {
            if (!client.Token.IsValid)
                throw new NotSupportedException("You must log in with oauth to get the current channel.");
            if (!client.Token.Authorization.Scopes.Contains("channel_read"))
                throw new MissingScopeException("channel_read");

            var model = await client.RestClient.GetCurrentChannelAsync();
            var entity = new RestSelfChannel(client, model.Id);
            entity.Update(model);
            return entity;
        }

        public static async Task<RestChannel> GetChannelAsync(BaseRestClient client, ulong channelId)
        {
            var model = await client.RestClient.GetChannelAsync(channelId);
            var entity = new RestChannel(client, model.Id);
            entity.Update(model);
            return entity;
        }

        public static async Task<IEnumerable<RestCheerInfo>> GetCheersAsync(BaseRestClient client, ulong? channelId)
        {
            var model = await client.RestClient.GetCheersAsync(channelId);
            var entity = model.Actions.Select(x => new RestCheerInfo(client, x));
            return entity;
        }

        #endregion
        #region Communities

        public static async Task<RestCommunity> GetCommunityAsync(BaseRestClient client, string id, bool isname = false)
        {
            var model = await client.RestClient.GetCommunityAsync(id, isname);
            var entity = new RestCommunity(client, model.Id);
            entity.Update(model);
            return entity;
        }

        #endregion
        #region Videos

        public static async Task<RestVideo> GetVideoAsync(BaseRestClient client, string id)
        {
            var model = await client.RestClient.GetVideoAsync(id);
            var entity = new RestVideo(client, model.Id);
            entity.Update(model);
            return entity;
        }

        #endregion
        #region Teams

        public static async Task<RestTeam> GetTeamAsync(BaseRestClient client, string name)
        {
            var model = await client.RestClient.GetTeamAsync(name);
            var entity = new RestTeam(client, model.Id);
            entity.Update(model);
            return entity;
        }

        public static async Task<IEnumerable<RestSimpleTeam>> GetTeamsAsync(BaseRestClient client, uint limit, uint offset)
        {
            var model = await client.RestClient.GetTeamsAsync(limit, offset);
            if (model == null)
                return new List<RestSimpleTeam>();

            var entity = model.Teams.Select(x =>
            {
                var team = new RestSimpleTeam(client, x.Id);
                team.Update(x);
                return team;
            });
            return entity;
        }

        #endregion
    }
}
