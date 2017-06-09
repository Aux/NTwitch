using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    internal static class ClientHelper
    {
        // Tokens
        public static async Task<RestTokenInfo> GetTokenInfoAsync(BaseTwitchClient client, RequestOptions options = null)
        {
            var model = await client.ApiClient.ValidateTokenAsync(options).ConfigureAwait(false);
            return RestTokenInfo.Create(client.ApiClient.AuthToken, model.Token);
        }

        // Channels
        public static async Task<RestSelfChannel> GetCurrentChannelAsync(BaseTwitchClient client, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetMyChannelAsync(options).ConfigureAwait(false);
            return RestSelfChannel.Create(client, model);
        }

        internal static async Task<IReadOnlyCollection<RestCheerInfo>> GetCheersAsync(BaseTwitchClient client, ulong? channelId, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetCheersAsync(channelId, options).ConfigureAwait(false);
            return model.Actions.Select(x => new RestCheerInfo(client, x)).ToArray();
        }

        public static async Task<RestChannel> GetChannelAsync(BaseTwitchClient client, ulong channelId, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetChannelAsync(channelId, options).ConfigureAwait(false);
            return RestChannel.Create(client, model);
        }

        public static async Task<IReadOnlyCollection<RestChannel>> FindChannelAsync(BaseTwitchClient client, string query, PageOptions paging = null, RequestOptions options = null)
        {
            var model = await client.ApiClient.FindChannelsAsync(query, paging, options).ConfigureAwait(false);
            return model.Channels.Select(x => RestChannel.Create(client, x)).ToArray();
        }
        
        // Communities
        public static async Task<RestCommunity> GetCommunityAsync(BaseTwitchClient client, string communityId, bool isname, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetCommunityAsync(communityId, isname, options).ConfigureAwait(false);
            return RestCommunity.Create(client, model);
        }

        public static async Task<IReadOnlyCollection<RestTopCommunity>> GetTopCommunitiesAsync(BaseTwitchClient client, PageOptions paging = null, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetTopCommunitiesAsync(paging, options).ConfigureAwait(false);
            return model.Communities.Select(x => RestTopCommunity.Create(client, x)).ToArray();
        }

        // Ingests
        public static async Task<IReadOnlyCollection<RestIngest>> GetIngestsAsync(BaseTwitchClient client, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetIngestsAsync(options).ConfigureAwait(false);
            return model.Ingests.Select(x => RestIngest.Create(client, x)).ToArray();
        }
        
        // Streams
        public static async Task<RestStream> GetStreamAsync(BaseTwitchClient client, ulong channelId, StreamType type, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetStreamAsync(channelId, type, options).ConfigureAwait(false);
            return RestStream.Create(client, model.Stream);
        }
        
        public static async Task<IReadOnlyCollection<RestStream>> GetStreamsAsync(BaseTwitchClient client, ulong[] channelIds, PageOptions paging = null, RequestOptions options = null)
        {
            var parameters = new GetStreamsParams() { ChannelIds = channelIds };
            return await GetStreamsAsync(client, x => { x = parameters; }, paging, options);
        }

        public static async Task<IReadOnlyCollection<RestStream>> GetStreamsAsync(BaseTwitchClient client, Action<GetStreamsParams> parameters, PageOptions paging = null, RequestOptions options = null)
        {
            var filledParams = new GetStreamsParams();
            parameters.Invoke(filledParams);

            var model = await client.ApiClient.GetStreamsAsync(filledParams, paging, options).ConfigureAwait(false);
            return model.Streams.Select(x => RestStream.Create(client, x)).ToArray();
        }
        
        public static async Task<RestGameSummary> GetGameSummaryAsync(BaseTwitchClient client, string game, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetStreamSummaryAsync(game, options).ConfigureAwait(false);
            return RestGameSummary.Create(model);
        }

        public static async Task<IReadOnlyCollection<RestFeaturedStream>> GetFeaturedStreamsAsync(BaseTwitchClient client, PageOptions paging, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetFeaturedStreamsAsync(paging, options).ConfigureAwait(false);
            return model.Featured.Select(x => RestFeaturedStream.Create(client, x)).ToArray();
        }

        public static async Task<IReadOnlyCollection<RestGame>> FindGamesAsync(BaseTwitchClient client, string query, bool islive, RequestOptions options = null)
        {
            var model = await client.ApiClient.FindGamesAsync(query, islive, options).ConfigureAwait(false);
            return model.Games.Select(x => RestGame.Create(client, x)).ToArray();
        }

        public static async Task<IReadOnlyCollection<RestStream>> FindStreamsAsync(BaseTwitchClient client, string query, bool? hls, PageOptions paging = null, RequestOptions options = null)
        {
            var model = await client.ApiClient.FindStreamsAsync(query, hls, paging, options).ConfigureAwait(false);
            return model.Streams.Select(x => RestStream.Create(client, x)).ToArray();
        }

        // Teams
        public static async Task<IReadOnlyCollection<RestSimpleTeam>> GetTeamsAsync(BaseTwitchClient client, PageOptions paging = null, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetTeamsAsync(paging, options).ConfigureAwait(false);
            return model.Teams.Select(x => RestSimpleTeam.Create(client, x)).ToArray();
        }

        public static async Task<RestTeam> GetTeamAsync(BaseTwitchClient client, string name, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetTeamAsync(name, options).ConfigureAwait(false);
            return RestTeam.Create(client, model);
        }

        // Users
        public static async Task<RestSelfUser> GetCurrentUserAsync(BaseTwitchClient client, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetMyUserAsync(options).ConfigureAwait(false);
            return RestSelfUser.Create(client, model);
        }

        public static async Task<RestUser> GetUserAsync(BaseTwitchClient client, ulong userId, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetUserAsync(userId, options).ConfigureAwait(false);
            return RestUser.Create(client, model);
        }

        public static async Task<IReadOnlyCollection<RestUser>> GetUsersAsync(BaseTwitchClient client, string[] usernames, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetUsersAsync(usernames, options).ConfigureAwait(false);
            return model.Users.Select(x => RestUser.Create(client, x)).ToArray();
        }

        // Videos
        public static async Task<RestVideo> GetVideoAsync(BaseTwitchClient client, string videoId, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetVideoAsync(videoId, options).ConfigureAwait(false);
            return RestVideo.Create(client, model);
        }

        public static async Task<IReadOnlyCollection<RestVideo>> GetTopVideosAsync(BaseTwitchClient client, string game, string period, string broadcastType, string language, string sort, PageOptions paging, RequestOptions options)
        {
            var model = await client.ApiClient.GetTopVideosAsync(game, period, broadcastType, language, sort, paging, options).ConfigureAwait(false);
            return model.Vods.Select(x => RestVideo.Create(client, x)).ToArray();
        }

        public static async Task<IReadOnlyCollection<RestVideo>> GetFollowedVideosAsync(BaseTwitchClient client, string broadcastType, string language, string sort, PageOptions paging, RequestOptions options)
        {
            var model = await client.ApiClient.GetFollowedVideosAsync(broadcastType, language, sort, paging, options).ConfigureAwait(false);
            return model.Videos.Select(x => RestVideo.Create(client, x)).ToArray();
        }

        public static async Task<RestClip> GetClipAsync(BaseTwitchClient client, string clipId, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetClipAsync(clipId, options).ConfigureAwait(false);
            return RestClip.Create(client, model);
        }

        public static async Task<IReadOnlyCollection<RestClip>> GetFollowedClipsAsync(BaseTwitchClient client, ulong id, bool istrending, PageOptions paging = null, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetFollowedClipsAsync(istrending, paging, options).ConfigureAwait(false);
            return model.Clips.Select(x => RestClip.Create(client, x)).ToArray();
        }

        public static async Task<IReadOnlyCollection<RestClip>> GetTopClipsAsync(BaseTwitchClient client, Action<TopClipsParams> parameters, RequestOptions options = null)
        {
            var filledParams = new TopClipsParams();
            parameters.Invoke(filledParams);

            var model = await client.ApiClient.GetTopClipsAsync(filledParams, options).ConfigureAwait(false);
            return model.Clips.Select(x => RestClip.Create(client, x)).ToArray();
        }
    }
}