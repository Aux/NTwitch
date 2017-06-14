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
            if (model.Token != null)
                return RestTokenInfo.Create(client.ApiClient.AuthToken, model.Token);
            return null;
        }

        // Channels
        public static async Task<RestSelfChannel> GetCurrentChannelAsync(BaseTwitchClient client, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetMyChannelAsync(options).ConfigureAwait(false);
            if (model != null)
                return RestSelfChannel.Create(client, model);
            return null;
        }

        internal static async Task<IReadOnlyCollection<RestCheerInfo>> GetCheersAsync(BaseTwitchClient client, ulong? channelId, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetCheersAsync(channelId, options).ConfigureAwait(false);
            if (model.Actions != null)
                return model.Actions.Select(x => new RestCheerInfo(client, x)).ToArray();
            return null;
        }

        public static async Task<RestChannel> GetChannelAsync(BaseTwitchClient client, ulong channelId, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetChannelAsync(channelId, options).ConfigureAwait(false);
            if (model != null)
                return RestChannel.Create(client, model);
            return null;
        }

        public static async Task<IReadOnlyCollection<RestChannel>> FindChannelAsync(BaseTwitchClient client, string query, PageOptions paging = null, RequestOptions options = null)
        {
            var model = await client.ApiClient.FindChannelsAsync(query, paging, options).ConfigureAwait(false);
            if (model.Channels != null)
                return model.Channels.Select(x => RestChannel.Create(client, x)).ToArray();
            return null;
        }
        
        // Communities
        public static async Task<RestCommunity> GetCommunityAsync(BaseTwitchClient client, string communityId, bool isname, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetCommunityAsync(communityId, isname, options).ConfigureAwait(false);
            if (model != null)
                return RestCommunity.Create(client, model);
            return null;
        }

        public static async Task<IReadOnlyCollection<RestTopCommunity>> GetTopCommunitiesAsync(BaseTwitchClient client, PageOptions paging = null, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetTopCommunitiesAsync(paging, options).ConfigureAwait(false);
            if (model.Communities != null)
                return model.Communities.Select(x => RestTopCommunity.Create(client, x)).ToArray();
            return null;
        }

        // Ingests
        public static async Task<IReadOnlyCollection<RestIngest>> GetIngestsAsync(BaseTwitchClient client, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetIngestsAsync(options).ConfigureAwait(false);
            if (model.Ingests != null)
                return model.Ingests.Select(x => RestIngest.Create(client, x)).ToArray();
            return null;
        }
        
        // Streams
        public static async Task<RestStream> GetStreamAsync(BaseTwitchClient client, ulong channelId, StreamType type, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetStreamAsync(channelId, type, options).ConfigureAwait(false);
            if (model.Stream != null)
                return RestStream.Create(client, model.Stream);
            return null;
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
            if (model.Streams != null)
                return model.Streams.Select(x => RestStream.Create(client, x)).ToArray();
            return null;
        }
        
        public static async Task<RestGameSummary> GetGameSummaryAsync(BaseTwitchClient client, string game, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetStreamSummaryAsync(game, options).ConfigureAwait(false);
            if (model != null)
                return RestGameSummary.Create(model);
            return null;
        }

        public static async Task<IReadOnlyCollection<RestFeaturedStream>> GetFeaturedStreamsAsync(BaseTwitchClient client, PageOptions paging, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetFeaturedStreamsAsync(paging, options).ConfigureAwait(false);
            if (model.Featured != null)
                return model.Featured.Select(x => RestFeaturedStream.Create(client, x)).ToArray();
            return null;
        }

        public static async Task<IReadOnlyCollection<RestStream>> GetFollowedStreamsAsync(BaseTwitchClient client, StreamType type, PageOptions paging, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetFollowedStreamsAsync(type, paging, options).ConfigureAwait(false);
            if (model.Streams != null)
                return model.Streams.Select(x => RestStream.Create(client, x)).ToArray();
            return null;
        }

        public static async Task<IReadOnlyCollection<RestGame>> FindGamesAsync(BaseTwitchClient client, string query, bool islive, RequestOptions options = null)
        {
            var model = await client.ApiClient.FindGamesAsync(query, islive, options).ConfigureAwait(false);
            if (model.Games != null)
                return model.Games.Select(x => RestGame.Create(client, x)).ToArray();
            return null;
        }

        public static async Task<IReadOnlyCollection<RestStream>> FindStreamsAsync(BaseTwitchClient client, string query, bool? hls, PageOptions paging = null, RequestOptions options = null)
        {
            var model = await client.ApiClient.FindStreamsAsync(query, hls, paging, options).ConfigureAwait(false);
            if (model.Streams != null)
                return model.Streams.Select(x => RestStream.Create(client, x)).ToArray();
            return null;
        }

        // Teams
        public static async Task<IReadOnlyCollection<RestSimpleTeam>> GetTeamsAsync(BaseTwitchClient client, PageOptions paging = null, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetTeamsAsync(paging, options).ConfigureAwait(false);
            if (model.Teams != null)
                return model.Teams.Select(x => RestSimpleTeam.Create(client, x)).ToArray();
            return null;
        }

        public static async Task<RestTeam> GetTeamAsync(BaseTwitchClient client, string name, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetTeamAsync(name, options).ConfigureAwait(false);
            if (model != null)
                return RestTeam.Create(client, model);
            return null;
        }

        // Users
        public static async Task<RestSelfUser> GetCurrentUserAsync(BaseTwitchClient client, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetMyUserAsync(options).ConfigureAwait(false);
            if (model != null)
                return RestSelfUser.Create(client, model);
            return null;
        }

        public static async Task<RestUser> GetUserAsync(BaseTwitchClient client, ulong userId, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetUserAsync(userId, options).ConfigureAwait(false);
            if (model != null)
                return RestUser.Create(client, model);
            return null;
        }

        public static async Task<IReadOnlyCollection<RestUser>> GetUsersAsync(BaseTwitchClient client, string[] usernames, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetUsersAsync(usernames, options).ConfigureAwait(false);
            if (model.Users != null)
                return model.Users.Select(x => RestUser.Create(client, x)).ToArray();
            return null;
        }

        // Videos
        public static async Task<RestVideo> GetVideoAsync(BaseTwitchClient client, string videoId, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetVideoAsync(videoId, options).ConfigureAwait(false);
            if (model != null)
                return RestVideo.Create(client, model);
            return null;
        }

        public static async Task<IReadOnlyCollection<RestVideo>> GetTopVideosAsync(BaseTwitchClient client, string game, string period, string broadcastType, string language, string sort, PageOptions paging, RequestOptions options)
        {
            var model = await client.ApiClient.GetTopVideosAsync(game, period, broadcastType, language, sort, paging, options).ConfigureAwait(false);
            if (model.Vods != null)
                return model.Vods.Select(x => RestVideo.Create(client, x)).ToArray();
            return null;
        }

        public static async Task<IReadOnlyCollection<RestVideo>> GetFollowedVideosAsync(BaseTwitchClient client, string broadcastType, string language, string sort, PageOptions paging, RequestOptions options)
        {
            var model = await client.ApiClient.GetFollowedVideosAsync(broadcastType, language, sort, paging, options).ConfigureAwait(false);
            if (model.Videos != null)
                return model.Videos.Select(x => RestVideo.Create(client, x)).ToArray();
            return null;
        }

        public static async Task<RestClip> GetClipAsync(BaseTwitchClient client, string clipId, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetClipAsync(clipId, options).ConfigureAwait(false);
            if (model != null)
                return RestClip.Create(client, model);
            return null;
        }

        public static async Task<IReadOnlyCollection<RestClip>> GetFollowedClipsAsync(BaseTwitchClient client, ulong id, bool istrending, PageOptions paging = null, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetFollowedClipsAsync(istrending, paging, options).ConfigureAwait(false);
            if (model.Clips != null)
                return model.Clips.Select(x => RestClip.Create(client, x)).ToArray();
            return null;
        }

        public static async Task<IReadOnlyCollection<RestClip>> GetTopClipsAsync(BaseTwitchClient client, Action<TopClipsParams> parameters, RequestOptions options = null)
        {
            var filledParams = new TopClipsParams();
            parameters.Invoke(filledParams);

            var model = await client.ApiClient.GetTopClipsAsync(filledParams, options).ConfigureAwait(false);
            if (model.Clips != null)
                return model.Clips.Select(x => RestClip.Create(client, x)).ToArray();
            return null;
        }
    }
}