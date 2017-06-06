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
            return RestToken.Create(client, model.Token, client.ApiClient.AuthToken);
        }

        // Clips
        public static Task<RestClip> GetClipAsync(TwitchRestClient client, string clipId)
        {
            throw new NotImplementedException();
        }

        public static Task<IReadOnlyCollection<RestClip>> GetTopClipsAsync(TwitchRestClient client, Action<TopClipsParams> options)
        {
            throw new NotImplementedException();
        }

        // Communities
        public static Task<RestCommunity> GetCommunityAsync(TwitchRestClient client, string communityId, bool isname)
        {
            throw new NotImplementedException();
        }

        public static Task<IReadOnlyCollection<RestTopCommunity>> GetTopCommunitiesAsync(TwitchRestClient client, uint limit)
        {
            throw new NotImplementedException();
        }

        // Channels
        public static Task<RestSelfChannel> GetCurrentChannelAsync(TwitchRestClient client)
        {
            throw new NotImplementedException();
        }

        public static Task<RestChannel> GetChannelAsync(TwitchRestClient client, ulong channelId)
        {
            throw new NotImplementedException();
        }

        public static Task<IReadOnlyCollection<RestChannel>> FindChannelAsync(TwitchRestClient client, string query, uint limit, uint offset)
        {
            throw new NotImplementedException();
        }
        
        // Ingests
        public static Task<IReadOnlyCollection<RestIngest>> GetIngestsAsync(TwitchRestClient client)
        {
            throw new NotImplementedException();
        }

        // Streams
        public static Task<RestStream> GetStreamAsync(TwitchRestClient client, ulong channelId, StreamType type)
        {
            throw new NotImplementedException();
        }
        
        public static Task<IReadOnlyCollection<RestStream>> GetStreamsAsync(TwitchRestClient client, ulong[] channels)
        {
            throw new NotImplementedException();
        }

        public static Task<IReadOnlyCollection<RestStream>> GetStreamsAsync(TwitchRestClient client, Action<GetStreamsParams> options)
        {
            throw new NotImplementedException();
        }
        
        public static Task<RestGameSummary> GetGameSummaryAsync(TwitchRestClient client, string game)
        {
            throw new NotImplementedException();
        }

        public static Task<IReadOnlyCollection<RestFeaturedStream>> GetFeaturedStreamsAsync(TwitchRestClient client, uint limit, uint offset)
        {
            throw new NotImplementedException();
        }

        public static Task<IReadOnlyCollection<RestGame>> FindGamesAsync(TwitchRestClient client, string query, bool islive)
        {
            throw new NotImplementedException();
        }

        public static Task<IReadOnlyCollection<RestStream>> FindStreamsAsync(TwitchRestClient client, string query, bool? hls, uint limit, uint offset)
        {
            throw new NotImplementedException();
        }

        // Teams
        public static Task<IReadOnlyCollection<RestSimpleTeam>> GetTeamsAsync(TwitchRestClient client, uint limit, uint offset)
        {
            throw new NotImplementedException();
        }

        public static Task<RestTeam> GetTeamAsync(TwitchRestClient client, string name)
        {
            throw new NotImplementedException();
        }

        // Users
        public static async Task<RestSelfUser> GetCurrentUserAsync(TwitchRestClient client, RequestOptions options = null)
        {
            var model = await client.ApiClient.GetMyUserAsync(options).ConfigureAwait(false);
            return RestSelfUser.Create(client, model);
        }

        public static Task<RestUser> GetUserAsync(TwitchRestClient client, ulong userId)
        {
            throw new NotImplementedException();
        }

        public static Task<IReadOnlyCollection<RestUser>> GetUsersAsync(TwitchRestClient client, string[] usernames)
        {
            throw new NotImplementedException();
        }

        // Videos
        public static Task<RestVideo> GetVideoAsync(TwitchRestClient client, string videoId)
        {
            throw new NotImplementedException();
        }
    }
}