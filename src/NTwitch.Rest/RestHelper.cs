using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    internal static class RestHelper
    {
        public static async Task<RestTokenInfo> AuthorizeAsync(BaseRestClient client, string token)
        {
            await client.Logger.InfoAsync("Rest", "Logging in...").ConfigureAwait(false);
            
            var model = await client.RestClient.AuthorizeAsync(token);
            var entity = RestTokenInfo.Create(model, token);
            
            await client.Logger.InfoAsync("Rest", "Login success!").ConfigureAwait(false);
            return entity;
        }

        public static async Task<IReadOnlyCollection<RestIngest>> GetIngestsAsync(BaseRestClient client)
        {
            string token = TokenHelper.GetSingleToken(client);
            var model = await client.RestClient.GetIngestsInternalAsync(token);

            var entity = model.Ingests.Select(x =>
            {
                var ingest = new RestIngest(client, x.Id);
                ingest.Update(x);
                return ingest;
            });
            return entity.ToArray();
        }

        #region Tokens

        public static RestTokenInfo GetTokenInfo(BaseRestClient client, ulong userId)
        {
            if (client.Tokens.TryGetValue(userId, out RestTokenInfo token))
                return token;
            else
                return null;
        }
        
        #endregion
        #region Search

        internal static async Task<IReadOnlyCollection<RestStream>> SearchStreamsAsync(BaseRestClient client, string query, bool? hls, uint limit, uint offset)
        {
            var token = TokenHelper.GetSingleToken(client);
            var model = await client.RestClient.SearchStreamsInternalAsync(token, query, hls, limit, offset);
            if (model == null)
                return new List<RestStream>();

            var entity = model.Streams.Select(x =>
            {
                var stream = new RestStream(client, x.Id);
                stream.Update(x);
                return stream;
            });
            return entity.ToArray();
        }

        internal static async Task<IReadOnlyCollection<RestGame>> SearchGamesAsync(BaseRestClient client, string query, bool islive)
        {
            var token = TokenHelper.GetSingleToken(client);
            var model = await client.RestClient.SearchGamesInternalAsync(token, query, islive);
            if (model == null)
                return new List<RestGame>();

            var entity = model.Games.Select(x =>
            {
                var game = new RestGame(client, x.Id);
                game.Update(x);
                return game;
            });
            return entity.ToArray();
        }

        internal static async Task<IReadOnlyCollection<RestChannel>> SearchChannelsAsync(BaseRestClient client, string query, uint limit, uint offset)
        {
            var token = TokenHelper.GetSingleToken(client);
            var model = await client.RestClient.SearchChannelsInternalAsync(token, query, limit, offset);
            if (model == null)
                return new List<RestChannel>();

            var entity = model.Channels.Select(x =>
            {
                var channel = new RestChannel(client, x.Id);
                channel.Update(x);
                return channel;
            });
            return entity.ToArray();
        }

        #endregion
        #region Users

        public static async Task<RestSelfUser> GetSelfUserAsync(BaseRestClient client, ulong userId)
        {
            if (TokenHelper.TryGetToken(client, userId, out RestTokenInfo info))
                throw new MissingScopeException("user_read");
            if (!info.Authorization.Scopes.Contains("user_read"))
                throw new MissingScopeException("user_read");

            var model = await client.RestClient.GetSelfUserInternalAsync(info.Token);
            var entity = new RestSelfUser(client, model.Id);
            entity.Update(model);
            return entity;
        }

        public static async Task<RestUser> GetUserAsync(BaseRestClient client, ulong id)
        {
            var token = TokenHelper.GetSingleToken(client);
            var model = await client.RestClient.GetUserInternalAsync(token, id);
            if (model == null)
                return null;

            var entity = new RestUser(client, model.Id);
            entity.Update(model);
            return entity;
        }

        public static async Task<IReadOnlyCollection<RestUser>> GetUsersAsync(BaseRestClient client, string[] usernames)
        {
            var token = TokenHelper.GetSingleToken(client);
            var model = await client.RestClient.GetUsersInternalAsync(token, usernames);
            if (model == null)
                return new List<RestUser>();

            var entity = model.Users.Select(x =>
            {
                var user = new RestUser(client, x.Id);
                user.Update(x);
                return user;
            });
            return entity.ToArray();
        }

        #endregion
        #region Channels

        public static async Task<RestSelfChannel> GetSelfChannelAsync(BaseRestClient client, ulong channelId)
        {
            if (!TokenHelper.TryGetToken(client, channelId, out RestTokenInfo info))
                throw new MissingScopeException("channel_read");
            if (!info.Authorization.Scopes.Contains("channel_read"))
                throw new MissingScopeException("channel_read");

            var model = await client.RestClient.GetSelfChannelInternalAsync(info.Token);
            var entity = new RestSelfChannel(client, model.Id);
            entity.Update(model);
            return entity;
        }

        public static async Task<RestChannel> GetChannelAsync(BaseRestClient client, ulong channelId)
        {
            var token = TokenHelper.GetSingleToken(client);
            var model = await client.RestClient.GetChannelInternalAsync(token, channelId);
            var entity = new RestChannel(client, model.Id);
            entity.Update(model);
            return entity;
        }

        public static async Task<IReadOnlyCollection<RestCheerInfo>> GetCheersAsync(BaseRestClient client, ulong? channelId)
        {
            var token = TokenHelper.GetSingleToken(client);
            var model = await client.RestClient.GetCheersInternalAsync(token, channelId);
            var entity = model.Actions.Select(x => new RestCheerInfo(client, x));
            return entity.ToArray();
        }

        #endregion
        #region Streams

        internal static async Task<IReadOnlyCollection<RestStream>> GetFollowedStreamsAsync(BaseRestClient client, ulong userId, StreamType type, uint limit, uint offset)
        {
            if (!TokenHelper.TryGetToken(client, userId, out RestTokenInfo info))
                throw new MissingScopeException("user_read");
            if (!info.Authorization.Scopes.Contains("user_read"))
                throw new MissingScopeException("user_read");

            var model = await client.RestClient.GetFollowedStreamsInternalAsync(info?.Token, type, limit, offset);
            if (model == null)
                return new List<RestStream>();

            var entity = model.Streams.Select(x =>
            {
                var stream = new RestStream(client, x.Id);
                stream.Update(x);
                return stream;
            });
            return entity.ToArray();
        }

        public static async Task<RestStream> GetStreamAsync(BaseRestClient client, ulong channelId, StreamType type)
        {
            var token = TokenHelper.GetSingleToken(client);
            var model = await client.RestClient.GetStreamInternalAsync(token, channelId, type);
            if (model.Stream == null)
                return null;
            
            var entity = new RestStream(client, model.Stream.Id);
            entity.Update(model.Stream);
            return entity;
        }

        internal static async Task<IReadOnlyCollection<RestStream>> GetStreamsAsync(BaseRestClient client, Action<GetStreamsParams> options)
        {
            var token = TokenHelper.GetSingleToken(client);

            var changes = new GetStreamsParams();
            options.Invoke(changes);

            var model = await client.RestClient.GetStreamsInternalAsync(token, changes);
            if (model == null)
                return new List<RestStream>();

            var entity = model.Streams.Select(x =>
            {
                var stream = new RestStream(client, x.Id);
                stream.Update(x);
                return stream;
            });
            return entity.ToArray();
        }

        internal static async Task<IReadOnlyCollection<RestFeaturedStream>> GetFeaturedStreamsAsync(BaseRestClient client, uint limit, uint offset)
        {
            var token = TokenHelper.GetSingleToken(client);
            var model = await client.RestClient.GetFeaturedStreamsInternalAsync(token, limit, offset);
            if (model == null)
                return new List<RestFeaturedStream>();

            var entity = model.Featured.Select(x =>
            {
                var stream = new RestFeaturedStream();
                stream.Update(client, x);
                return stream;
            });
            return entity.ToArray();
        }

        internal static async Task<RestGameSummary> GetGameSummaryAsync(BaseRestClient client, string game)
        {
            var token = TokenHelper.GetSingleToken(client);
            var model = await client.RestClient.GetGameSummaryInternalAsync(token, game);
            if (model == null)
                return null;

            var entity = new RestGameSummary();
            entity.Update(model);
            return entity;
        }

        #endregion
        #region Communities

        public static async Task<RestCommunity> GetCommunityAsync(BaseRestClient client, string id, bool isname = false)
        {
            var token = TokenHelper.GetSingleToken(client);
            var model = await client.RestClient.GetCommunityInternalAsync(token, id, isname);
            if (model == null)
                return null;

            var entity = new RestCommunity(client, model.Id);
            entity.Update(model);
            return entity;
        }

        public static async Task<IReadOnlyCollection<RestTopCommunity>> GetTopCommunitiesAsync(BaseRestClient client, uint limit)
        {
            var token = TokenHelper.GetSingleToken(client);
            var model = await client.RestClient.GetTopCommunitiesInternalAsync(token, limit);

            var entity = model.Communities.Select(x =>
            {
                var community = new RestTopCommunity(client, x.Id);
                community.Update(x);
                return community;
            });
            return entity.ToArray();
        }

        #endregion
        #region Videos

        public static async Task<RestVideo> GetVideoAsync(BaseRestClient client, string id)
        {
            var token = TokenHelper.GetSingleToken(client);
            var model = await client.RestClient.GetVideoInternalAsync(token, id);
            var entity = new RestVideo(client, model.Id);
            entity.Update(model);
            return entity;
        }

        #endregion
        #region Teams

        public static async Task<RestTeam> GetTeamAsync(BaseRestClient client, string name)
        {
            var token = TokenHelper.GetSingleToken(client);
            var model = await client.RestClient.GetTeamInternalAsync(token, name);
            var entity = new RestTeam(client, model.Id);
            entity.Update(model);
            return entity;
        }

        public static async Task<IReadOnlyCollection<RestSimpleTeam>> GetTeamsAsync(BaseRestClient client, uint limit, uint offset)
        {
            var token = TokenHelper.GetSingleToken(client);
            var model = await client.RestClient.GetTeamsInternalAsync(token, limit, offset);
            if (model == null)
                return new List<RestSimpleTeam>();

            var entity = model.Teams.Select(x =>
            {
                var team = new RestSimpleTeam(client, x.Id);
                team.Update(x);
                return team;
            });
            return entity.ToArray();
        }

        #endregion
    }
}
