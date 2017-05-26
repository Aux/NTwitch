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
            var entity = RestTokenInfo.Create(client, model, token);
            
            await client.Logger.InfoAsync("Rest", "Login success!").ConfigureAwait(false);
            return entity;
        }

        public static async Task<IReadOnlyCollection<RestIngest>> GetIngestsAsync(BaseRestClient client)
        {
            string token = TokenHelper.GetSingleToken(client);
            var model = await client.RestClient.GetIngestsInternalAsync(token);

            var entity = model.Ingests.Select(x => RestIngest.Create(client, x));
            return entity.ToArray();
        }

        #region Tokens

        public static RestTokenInfo GetTokenInfo(BaseRestClient client, ulong userId)
        {
            if (TokenHelper.TryGetToken(client, userId, out RestTokenInfo token))
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

            var entity = model.Streams.Select(x => RestStream.Create(client, x));
            return entity.ToArray();
        }

        internal static async Task<IReadOnlyCollection<RestGame>> SearchGamesAsync(BaseRestClient client, string query, bool islive)
        {
            var token = TokenHelper.GetSingleToken(client);
            var model = await client.RestClient.SearchGamesInternalAsync(token, query, islive);
            if (model == null)
                return new List<RestGame>();

            var entity = model.Games.Select(x => RestGame.Create(client, x));
            return entity.ToArray();
        }

        internal static async Task<IReadOnlyCollection<RestChannel>> SearchChannelsAsync(BaseRestClient client, string query, uint limit, uint offset)
        {
            var token = TokenHelper.GetSingleToken(client);
            var model = await client.RestClient.SearchChannelsInternalAsync(token, query, limit, offset);
            if (model == null)
                return new List<RestChannel>();

            var entity = model.Channels.Select(x => RestChannel.Create(client, x));
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
            return RestSelfUser.Create(client, model);
        }

        public static async Task<RestUser> GetUserAsync(BaseRestClient client, ulong id)
        {
            var token = TokenHelper.GetSingleToken(client);
            var model = await client.RestClient.GetUserInternalAsync(token, id);
            if (model == null)
                return null;

            return RestUser.Create(client, model);
        }

        public static async Task<IReadOnlyCollection<RestUser>> GetUsersAsync(BaseRestClient client, string[] usernames)
        {
            var token = TokenHelper.GetSingleToken(client);
            var model = await client.RestClient.GetUsersInternalAsync(token, usernames);
            if (model == null)
                return new List<RestUser>();

            var entity = model.Users.Select(x => RestUser.Create(client, x));
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
            return RestSelfChannel.Create(client, model);
        }

        public static async Task<RestChannel> GetChannelAsync(BaseRestClient client, ulong channelId)
        {
            var token = TokenHelper.GetSingleToken(client);
            var model = await client.RestClient.GetChannelInternalAsync(token, channelId);
            return RestChannel.Create(client, model);
        }

        public static async Task<IReadOnlyCollection<RestCheerInfo>> GetCheersAsync(BaseRestClient client, ulong? channelId)
        {
            var token = TokenHelper.GetSingleToken(client);
            var model = await client.RestClient.GetCheersInternalAsync(token, channelId);
            var entity = model.Actions.Select(x => new RestCheerInfo(client, x));
            return entity.ToArray();
        }

        #endregion
        #region Clips

        public static async Task<RestClip> GetClipAsync(BaseRestClient client, string clipId)
        {
            var token = TokenHelper.GetSingleToken(client);
            var model = await client.RestClient.GetClipInternalAsync(token, clipId);
            return RestClip.Create(client, model);
        }

        public static async Task<IReadOnlyCollection<RestClip>> GetTopClipsAsync(BaseRestClient client, Action<TopClipsParams> options)
        {
            var token = TokenHelper.GetSingleToken(client);

            var properties = new TopClipsParams();
            options.Invoke(properties);

            var model = await client.RestClient.GetTopClipsInternalAsync(token, properties);
            var entity = model.Clips.Select(x => RestClip.Create(client, x));
            return entity.ToArray();
        }

        public static async Task<IReadOnlyCollection<RestClip>> GetFollowedClipsAsync(BaseRestClient client, ulong userId, bool istrending, uint limit)
        {
            if (!TokenHelper.TryGetToken(client, userId, out RestTokenInfo info))
                throw new MissingScopeException("user_read");
            if (!info.Authorization.Scopes.Contains("user_read"))
                throw new MissingScopeException("user_read");

            var model = await client.RestClient.GetFollowedClipsInternalAsync(info.Token, istrending, limit);
            var entity = model.Clips.Select(x => RestClip.Create(client, x));
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

            var entity = model.Streams.Select(x => RestStream.Create(client, x));
            return entity.ToArray();
        }

        public static async Task<RestStream> GetStreamAsync(BaseRestClient client, ulong channelId, StreamType type)
        {
            var token = TokenHelper.GetSingleToken(client);
            var model = await client.RestClient.GetStreamInternalAsync(token, channelId, type);
            if (model.Stream == null)
                return null;
            
            return RestStream.Create(client, model.Stream);
        }

        internal static Task<IReadOnlyCollection<RestStream>> GetStreamsAsync(BaseRestClient client, ulong[] channels)
        {
            return GetStreamsAsync(client, x =>
            {
                x.ChannelIds = channels;
            });
        }

        internal static async Task<IReadOnlyCollection<RestStream>> GetStreamsAsync(BaseRestClient client, Action<GetStreamsParams> options)
        {
            var token = TokenHelper.GetSingleToken(client);

            var changes = new GetStreamsParams();
            options.Invoke(changes);

            var model = await client.RestClient.GetStreamsInternalAsync(token, changes);
            if (model == null)
                return new List<RestStream>();

            var entity = model.Streams.Select(x => RestStream.Create(client, x));
            return entity.ToArray();
        }

        internal static async Task<IReadOnlyCollection<RestFeaturedStream>> GetFeaturedStreamsAsync(BaseRestClient client, uint limit, uint offset)
        {
            var token = TokenHelper.GetSingleToken(client);
            var model = await client.RestClient.GetFeaturedStreamsInternalAsync(token, limit, offset);
            if (model == null)
                return new List<RestFeaturedStream>();

            var entity = model.Featured.Select(x => RestFeaturedStream.Create(client, x));
            return entity.ToArray();
        }

        internal static async Task<RestGameSummary> GetGameSummaryAsync(BaseRestClient client, string game)
        {
            var token = TokenHelper.GetSingleToken(client);
            var model = await client.RestClient.GetGameSummaryInternalAsync(token, game);
            if (model == null)
                return null;

            return RestGameSummary.Create(model);
        }

        #endregion
        #region Communities

        public static async Task<RestCommunity> GetCommunityAsync(BaseRestClient client, string id, bool isname = false)
        {
            var token = TokenHelper.GetSingleToken(client);
            var model = await client.RestClient.GetCommunityInternalAsync(token, id, isname);
            if (model == null)
                return null;

            return RestCommunity.Create(client, model);
        }

        public static async Task<IReadOnlyCollection<RestTopCommunity>> GetTopCommunitiesAsync(BaseRestClient client, uint limit)
        {
            var token = TokenHelper.GetSingleToken(client);
            var model = await client.RestClient.GetTopCommunitiesInternalAsync(token, limit);

            var entity = model.Communities.Select(x => RestTopCommunity.Create(client, x));
            return entity.ToArray();
        }

        #endregion
        #region Videos

        public static async Task<RestVideo> GetVideoAsync(BaseRestClient client, string id)
        {
            var token = TokenHelper.GetSingleToken(client);
            var model = await client.RestClient.GetVideoInternalAsync(token, id);
            return RestVideo.Create(client, model);
        }

        #endregion
        #region Teams

        public static async Task<RestTeam> GetTeamAsync(BaseRestClient client, string name)
        {
            var token = TokenHelper.GetSingleToken(client);
            var model = await client.RestClient.GetTeamInternalAsync(token, name);
            return RestTeam.Create(client, model);
        }

        public static async Task<IReadOnlyCollection<RestSimpleTeam>> GetTeamsAsync(BaseRestClient client, uint limit, uint offset)
        {
            var token = TokenHelper.GetSingleToken(client);
            var model = await client.RestClient.GetTeamsInternalAsync(token, limit, offset);
            if (model == null)
                return new List<RestSimpleTeam>();

            var entity = model.Teams.Select(x => RestSimpleTeam.Create(client, x));
            return entity.ToArray();
        }

        #endregion
    }
}
