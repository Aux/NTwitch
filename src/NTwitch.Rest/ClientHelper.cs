using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    internal static class ClientHelper
    {
        // Tokens
        public static async Task<RestToken> GetTokenInfoAsync(TwitchRestClient client, RequestOptions options = null)
        {
            var model = await client.ApiClient.ValidateTokenAsync(options).ConfigureAwait(false);
            return RestToken.Create(client.ApiClient.AuthToken, model.Token);
        }

        // Channels
        public static async Task<RestSelfChannel> GetCurrentChannelAsync(TwitchRestClient client, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetMyChannelAsync(options).ConfigureAwait(false);
            return RestSelfChannel.Create(client, model);
        }

        internal static async Task<IReadOnlyCollection<RestCheerInfo>> GetCheersAsync(TwitchRestClient client, ulong? channelId, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetCheersAsync(channelId, options).ConfigureAwait(false);
            return model.Actions.Select(x => new RestCheerInfo(client, x)).ToArray();
        }

        public static async Task<RestChannel> GetChannelAsync(TwitchRestClient client, ulong channelId, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetChannelAsync(channelId, options).ConfigureAwait(false);
            return RestChannel.Create(client, model);
        }

        public static async Task<IReadOnlyCollection<RestChannel>> FindChannelAsync(TwitchRestClient client, string query, PageOptions paging = null, RequestOptions options = null)
        {
            var model = await client.ApiClient.FindChannelsAsync(query, paging, options).ConfigureAwait(false);
            return model.Channels.Select(x => RestChannel.Create(client, x)).ToArray();
        }
        
        // Communities
        public static async Task<RestCommunity> GetCommunityAsync(TwitchRestClient client, string communityId, bool isname, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetCommunityAsync(communityId, isname, options).ConfigureAwait(false);
            return RestCommunity.Create(client, model);
        }

        public static async Task<IReadOnlyCollection<RestTopCommunity>> GetTopCommunitiesAsync(TwitchRestClient client, PageOptions paging = null, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetTopCommunitiesAsync(paging, options).ConfigureAwait(false);
            return model.Communities.Select(x => RestTopCommunity.Create(client, x)).ToArray();
        }

        // Ingests
        public static async Task<IReadOnlyCollection<RestIngest>> GetIngestsAsync(TwitchRestClient client, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetIngestsAsync(options).ConfigureAwait(false);
            return model.Ingests.Select(x => RestIngest.Create(client, x)).ToArray();
        }
        
        // Streams
        public static async Task<RestStream> GetStreamAsync(TwitchRestClient client, ulong channelId, StreamType type, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetStreamAsync(channelId, type, options).ConfigureAwait(false);
            return RestStream.Create(client, model.Stream);
        }
        
        public static async Task<IReadOnlyCollection<RestStream>> GetStreamsAsync(TwitchRestClient client, ulong[] channelIds, PageOptions paging = null, RequestOptions options = null)
        {
            var parameters = new GetStreamsParams() { ChannelIds = channelIds };
            return await GetStreamsAsync(client, x => { x = parameters; }, paging, options);
        }

        public static async Task<IReadOnlyCollection<RestStream>> GetStreamsAsync(TwitchRestClient client, Action<GetStreamsParams> parameters, PageOptions paging = null, RequestOptions options = null)
        {
            var filledParams = new GetStreamsParams();
            parameters.Invoke(filledParams);

            var model = await client.ApiClient.GetStreamsAsync(filledParams, paging, options).ConfigureAwait(false);
            return model.Streams.Select(x => RestStream.Create(client, x)).ToArray();
        }
        
        public static async Task<RestGameSummary> GetGameSummaryAsync(TwitchRestClient client, string game, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetStreamSummaryAsync(game, options).ConfigureAwait(false);
            return RestGameSummary.Create(model);
        }

        public static async Task<IReadOnlyCollection<RestFeaturedStream>> GetFeaturedStreamsAsync(TwitchRestClient client, PageOptions paging, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetFeaturedStreamsAsync(paging, options).ConfigureAwait(false);
            return model.Featured.Select(x => RestFeaturedStream.Create(client, x)).ToArray();
        }

        public static async Task<IReadOnlyCollection<RestGame>> FindGamesAsync(TwitchRestClient client, string query, bool islive, RequestOptions options = null)
        {
            var model = await client.ApiClient.FindGamesAsync(query, islive, options).ConfigureAwait(false);
            return model.Games.Select(x => RestGame.Create(client, x)).ToArray();
        }

        public static async Task<IReadOnlyCollection<RestStream>> FindStreamsAsync(TwitchRestClient client, string query, bool? hls, PageOptions paging = null, RequestOptions options = null)
        {
            var model = await client.ApiClient.FindStreamsAsync(query, hls, paging, options).ConfigureAwait(false);
            return model.Streams.Select(x => RestStream.Create(client, x)).ToArray();
        }

        // Teams
        public static async Task<IReadOnlyCollection<RestSimpleTeam>> GetTeamsAsync(TwitchRestClient client, PageOptions paging = null, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetTeamsAsync(paging, options).ConfigureAwait(false);
            return model.Teams.Select(x => RestSimpleTeam.Create(client, x)).ToArray();
        }

        public static async Task<RestTeam> GetTeamAsync(TwitchRestClient client, string name, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetTeamAsync(name, options).ConfigureAwait(false);
            return RestTeam.Create(client, model);
        }

        // Users
        public static async Task<RestSelfUser> GetCurrentUserAsync(TwitchRestClient client, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetMyUserAsync(options).ConfigureAwait(false);
            return RestSelfUser.Create(client, model);
        }

        public static async Task<RestUser> GetUserAsync(TwitchRestClient client, ulong userId, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetUserAsync(userId, options).ConfigureAwait(false);
            return RestUser.Create(client, model);
        }

        public static async Task<IReadOnlyCollection<RestUser>> GetUsersAsync(TwitchRestClient client, string[] usernames, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetUsersAsync(usernames, options).ConfigureAwait(false);
            return model.Users.Select(x => RestUser.Create(client, x)).ToArray();
        }

        // Videos
        public static async Task<RestVideo> GetVideoAsync(TwitchRestClient client, string videoId, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetVideoAsync(videoId, options).ConfigureAwait(false);
            return RestVideo.Create(client, model);
        }

        public static async Task<RestClip> GetClipAsync(TwitchRestClient client, string clipId, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetClipAsync(clipId, options).ConfigureAwait(false);
            return RestClip.Create(client, model);
        }

        public static async Task<IReadOnlyCollection<RestClip>> GetFollowedClipsAsync(TwitchRestClient client, ulong id, bool istrending, PageOptions paging = null, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetFollowedClipsAsync(istrending, paging, options).ConfigureAwait(false);
            return model.Clips.Select(x => RestClip.Create(client, x)).ToArray();
        }

        public static async Task<IReadOnlyCollection<RestClip>> GetTopClipsAsync(TwitchRestClient client, Action<TopClipsParams> parameters, RequestOptions options = null)
        {
            var filledParams = new TopClipsParams();
            parameters.Invoke(filledParams);

            var model = await client.ApiClient.GetTopClipsAsync(filledParams, options).ConfigureAwait(false);
            return model.Clips.Select(x => RestClip.Create(client, x)).ToArray();
        }
    }
}