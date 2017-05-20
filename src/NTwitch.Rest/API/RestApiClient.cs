using System;
using System.Collections.Concurrent;
using System.Net;
using System.Threading.Tasks;

namespace NTwitch.Rest.API
{
    public class RestApiClient
    {
        public string RestHost => _client.RestHost;
        
        private RestClient _client;

        internal LogManager Logger;
        internal string ClientId;
        
        public RestApiClient(TwitchRestConfig config)
        {
            Logger = new LogManager(config.LogLevel);
            ClientId = config.ClientId;
            _client = new RestClient(config.RestHost, config.ClientId);
        }

        public Task<RestResponse> SendAsync(string method, string endpoint, string token)
            => SendAsync(new RestRequest(method, endpoint, token));
        public async Task<RestResponse> SendAsync(RestRequest request)
        {
            await Logger.DebugAsync("Rest", $"Attempting {request.Method} /{request.Endpoint}").ConfigureAwait(false);

            if (!request.HasToken && string.IsNullOrWhiteSpace(ClientId))
                throw new InvalidOperationException("No oauth token or client id specified");

            var msg = request.GetRequest();
            var response = await _client.SendAsync(msg);

            await Logger.DebugAsync("Rest", $"{request.Method} /{request.Endpoint} in {response.ExecuteTime}ms").ConfigureAwait(false);
            return response;
        }

        //
        // Authorization
        //

        internal async Task<Token> AuthorizeAsync(string token)
        {
            try
            {
                var response = await SendAsync("GET", "", token);
                return response.GetBodyAsType<TokenCollection>().Token;
            }
            catch (HttpException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
            {
                return new Token();
            }
            catch (HttpException ex) when (ex.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new InvalidOperationException("Specified string is not a valid token or client id");
            }
        }

        //
        // Clips
        //

        internal async Task<Clip> GetClipInternalAsync(string token, string clipId)
        {
            try
            {
                var response = await SendAsync("GET", $"clips/{clipId}", token);
                return response.GetBodyAsType<Clip>();
            }
            catch (HttpException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized || ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        internal async Task<ClipCollection> GetTopClipsInternalAsync(string token, TopClipsParams options)
        {
            try
            {
                var response = await SendAsync(new GetTopClipsRequest(token, options));
                return response.GetBodyAsType<ClipCollection>();
            }
            catch (HttpException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
            {
                return null;
            }
        }

        internal async Task<ClipCollection> GetFollowedClipsInternalAsync(string token, bool istrending, uint limit)
        {
            try
            {
                var response = await SendAsync(new GetFollowedClipsRequest(token, istrending, limit));
                return response.GetBodyAsType<ClipCollection>();
            }
            catch (HttpException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
            {
                return null;
            }
        }

        //
        // Channels
        //

        internal async Task<Channel> GetSelfChannelInternalAsync(string token)
        {
            try
            {
                var response = await SendAsync("GET", "channel", token);
                return response.GetBodyAsType<Channel>();
            }
            catch (HttpException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
            {
                return null;
            }
        }

        internal async Task<Channel> GetChannelInternalAsync(string token, ulong channelId)
        {
            try
            {
                var response = await SendAsync("GET", $"channels/{channelId}", token);
                return response.GetBodyAsType<Channel>();
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 422)
            {
                return null;
            }
        }

        internal async Task<Subscription> GetSubscriberInternalAsync(string token, ulong channelId, ulong userId)
        {
            try
            {
                var response = await SendAsync("GET", $"channels/{channelId}/subscriptions/{userId}", token);
                return response.GetBodyAsType<Subscription>();
            }
            catch (HttpException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized || ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }
        
        internal async Task<Channel> ModifyChannelInternalAsync(string token, ulong channelId, ModifyChannel changes)
        {
            try
            {
                var response = await SendAsync(new ModifyChannelRequest(token, channelId, changes));
                return response.GetBodyAsType<Channel>();
            }
            catch (HttpException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
            {
                return null;
            }
        }

        internal async Task<TeamCollection> GetChannelTeamsInternalAsync(string token, ulong channelId)
        {
            var response = await SendAsync("GET", $"channels/{channelId}/teams", token);
            return response.GetBodyAsType<TeamCollection>();
        }

        internal async Task<UserCollection> GetChannelEditorsAsync(string token, ulong channelId)
        {
            try
            {
                var response = await SendAsync("GET", $"channels/{channelId}/editors", token);
                return response.GetBodyAsType<UserCollection>();
            }
            catch (HttpException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
            {
                return null;
            }
        }

        internal async Task<FollowCollection> GetChannelFollowersInternalAsync(string token, ulong channelId, bool ascending, uint limit, uint offset)
        {
            var response = await SendAsync(new GetChannelFollowersRequest(token, channelId, ascending, limit, offset));
            return response.GetBodyAsType<FollowCollection>();
        }

        internal async Task<SubscriptionCollection> GetChannelSubscribersInternalAsync(string token, ulong channelId, bool ascending, uint limit, uint offset)
        {
            try
            {
                var response = await SendAsync(new GetChannelSubscribersRequest(token, channelId, ascending, limit, offset));
                return response.GetBodyAsType<SubscriptionCollection>();
            }
            catch (HttpException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
            {
                return null;
            }
        }

        //
        // Chat
        //

        internal async Task<CheerCollection> GetCheersInternalAsync(string token, ulong? channelId)
        {
            var response = await SendAsync(new GetCheersRequest(token, channelId));
            return response.GetBodyAsType<CheerCollection>();
        }

        internal async Task<ChatBadges> GetBadgesInternalAsync(string token, ulong channelId)
        {
            var response = await SendAsync("GET", $"chat/{channelId}/badges", token);
            return response.GetBodyAsType<ChatBadges>();
        }

        //
        // Community
        //
        
        internal async Task<Community> GetCommunityInternalAsync(string token, string communityId, bool isname)
        {
            try
            {
                string endpoint;
                if (isname)
                    endpoint = $"communities?name={communityId}";
                else
                    endpoint = $"communities/{communityId}";

                var response = await SendAsync("GET", endpoint, token);
                return response.GetBodyAsType<Community>();
            }
            catch (HttpException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        internal async Task<CommunityCollection> GetTopCommunitiesInternalAsync(string token, uint limit)
        {
            try
            {
                var response = await SendAsync("GET", $"communities/top", token);
                return response.GetBodyAsType<CommunityCollection>();
            }
            catch (HttpException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
            {
                return null;
            }
        }

        internal async Task<CommunityPermissions> GetCommunityPermissionsInternalAsync(string token, string communityId)
        {
            try
            {
                var response = await SendAsync("GET", $"communities/{communityId}/permissions", token);
                return response.GetBodyAsType<CommunityPermissions>();
            }
            catch (HttpException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        internal async Task CommunityReportInternalAsync(string token, string communityId, ulong channelId)
        {
            try
            {
                var response = await SendAsync(new CommunityReportRequest(token, communityId, channelId));
            }
            catch (HttpException ex) when (ex.StatusCode == HttpStatusCode.NoContent)
            {
                return;
            }
        }

        internal async Task ModifyCommunityInternalAsync(string token, string communityId, ModifyCommunityParams changes)
        {
            try
            {
                var response = await SendAsync(new ModifyCommunityRequest(token, communityId, changes));
            }
            catch (HttpException ex) when (ex.StatusCode == HttpStatusCode.NoContent)
            {
                return;
            }
        }

        internal async Task SetAvatarInternalAsync(string token, string communityId, string imageString)
        {
            try
            {
                var response = await SendAsync(new SetCommunityAvatarRequest(token, communityId, imageString));
            }
            catch (HttpException ex) when (ex.StatusCode == HttpStatusCode.NoContent)
            {
                return;
            }
        }

        internal async Task RemoveAvatarInternalAsync(string token, string communityId)
        {
            try
            {
                var response = await SendAsync("DELETE", $"communities/{communityId}/images/avatar", token);
            }
            catch (HttpException ex) when (ex.StatusCode == HttpStatusCode.NoContent)
            {
                return;
            }
        }

        internal async Task SetCoverInternalAsync(string token, string communityId, string imageString)
        {
            try
            {
                var response = await SendAsync(new SetCommunityCoverRequest(token, communityId, imageString));
            }
            catch (HttpException ex) when (ex.StatusCode == HttpStatusCode.NoContent)
            {
                return;
            }
        }

        internal async Task RemoveCoverInternalAsync(string token, string communityId)
        {
            try
            {
                var response = await SendAsync("DELETE", $"communities/{communityId}/images/cover", token);
            }
            catch (HttpException ex) when (ex.StatusCode == HttpStatusCode.NoContent)
            {
                return;
            }
        }

        internal async Task<CommunityCollection> GetCommunityModeratorsInternalAsync(string token, string communityId)
        {
            try
            {
                var response = await SendAsync("GET", $"communities/{communityId}/moderators", token);
                return response.GetBodyAsType<CommunityCollection>();
            }
            catch (HttpException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
            {
                return null;
            }
        }

        internal async Task RemoveCommunityModeratorInternalAsync(string token, string communityId, ulong userId)
        {
            try
            {
                var response = await SendAsync("PUT", $"communities/{communityId}/moderators/{userId}", token);
            }
            catch (HttpException ex) when (ex.StatusCode == HttpStatusCode.NoContent)
            {
                return;
            }
        }

        internal async Task AddCommunityModeratorInternalAsync(string token, string communityId, ulong userId)
        {
            try
            {
                var response = await SendAsync("DELETE", $"communities/{communityId}/moderators/{userId}", token);
            }
            catch (HttpException ex) when (ex.StatusCode == HttpStatusCode.NoContent)
            {
                return;
            }
        }

        internal async Task<CommunityCollection> GetCommunityBansInternalAsync(string token, string communityId, uint limit)
        {
            try
            {
                var response = await SendAsync(new GetCommunityBansRequest(token, communityId, limit));
                return response.GetBodyAsType<CommunityCollection>();
            }
            catch (HttpException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
            {
                return null;
            }
        }

        internal async Task AddCommunityBanInternalAsync(string token, string communityId, ulong userId)
        {
            try
            {
                var response = await SendAsync("PUT", $"communities/{communityId}/bans/{userId}", token);
            }
            catch (HttpException ex) when (ex.StatusCode == HttpStatusCode.NoContent)
            {
                return;
            }
        }

        internal async Task RemoveCommunityBanInternalAsync(string token, string communityId, ulong userId)
        {
            try
            {
                var response = await SendAsync("DELETE", $"communities/{communityId}/bans/{userId}", token);
            }
            catch (HttpException ex) when (ex.StatusCode == HttpStatusCode.NoContent)
            {
                return;
            }
        }

        internal async Task<CommunityCollection> GetCommunityTimeoutsInternalAsync(string token, string communityId, uint limit)
        {
            try
            {
                var response = await SendAsync(new GetCommunityTimeoutsRequest(token, communityId, limit));
                return response.GetBodyAsType<CommunityCollection>();
            }
            catch (HttpException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
            {
                return null;
            }
        }

        internal async Task AddCommunityTimeoutInternalAsync(string token, string communityId, ulong userId)
        {
            try
            {
                var response = await SendAsync("PUT", $"communities/{communityId}/timeouts/{userId}", token);
            }
            catch (HttpException ex) when (ex.StatusCode == HttpStatusCode.NoContent)
            {
                return;
            }
        }

        internal async Task RemoveCommunityTimeoutInternalAsync(string token, string communityId, ulong userId)
        {
            try
            {
                var response = await SendAsync("DELETE", $"communities/{communityId}/timeouts/{userId}", token);
            }
            catch (HttpException ex) when (ex.StatusCode == HttpStatusCode.NoContent)
            {
                return;
            }
        }
        
        //
        // Ingests
        //

        internal async Task<IngestCollection> GetIngestsInternalAsync(string token)
        {
            var response = await SendAsync("GET", "ingests", token);
            return response.GetBodyAsType<IngestCollection>();
        }

        //
        // Search
        //

        internal async Task<ChannelCollection> SearchChannelsInternalAsync(string token, string query, uint limit, uint offset)
        {
            var response = await SendAsync(new SearchChannelsRequest(token, query, limit, offset));
            return response.GetBodyAsType<ChannelCollection>();
        }

        internal async Task<StreamCollection> SearchStreamsInternalAsync(string token, string query, bool? hls, uint limit, uint offset)
        {
            var response = await SendAsync(new SearchStreamsRequest(token, query, hls, limit, offset));
            return response.GetBodyAsType<StreamCollection>();
        }

        internal async Task<GameCollection> SearchGamesInternalAsync(string token, string query, bool islive)
        {
            var response = await SendAsync(new SearchGamesRequest(token, query, islive));
            return response.GetBodyAsType<GameCollection>();
        }

        //
        // Streams
        //
        
        internal async Task<StreamCollection> GetFollowedStreamsInternalAsync(string token, StreamType type, uint limit, uint offset)
        {
            var response = await SendAsync(new GetFollowedStreamsRequest(token, type, limit, offset));
            return response.GetBodyAsType<StreamCollection>();
        }

        internal async Task<StreamCollection> GetStreamInternalAsync(string token, ulong channelId, StreamType type)
        {
            var response = await SendAsync(new GetStreamRequest(token, channelId, type));
            return response.GetBodyAsType<StreamCollection>();
        }

        internal async Task<StreamCollection> GetStreamsInternalAsync(string token, GetStreamsParams changes)
        {
            var response = await SendAsync(new GetStreamsRequest(token, changes));
            return response.GetBodyAsType<StreamCollection>();
        }

        internal async Task<StreamCollection> GetFeaturedStreamsInternalAsync(string token, uint limit, uint offset)
        {
            var response = await SendAsync(new GetFeaturedStreamsRequest(token, limit, offset));
            return response.GetBodyAsType<StreamCollection>();
        }

        internal async Task<Stream> GetGameSummaryInternalAsync(string token, string game)
        {
            var response = await SendAsync(new GetStreamSummaryRequest(token, game));
            return response.GetBodyAsType<Stream>();
        }

        //
        // Teams
        //

        internal async Task<Team> GetTeamInternalAsync(string token, string name)
        {
            try
            {
                var response = await SendAsync("GET", $"teams/{name}", token);
                return response.GetBodyAsType<Team>();
            }
            catch (HttpException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        internal async Task<TeamCollection> GetTeamsInternalAsync(string token, uint limit, uint offset)
        {
            try
            {
                var response = await SendAsync("GET", $"teams", token);
                return response.GetBodyAsType<TeamCollection>();
            }
            catch (HttpException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        //
        // Users
        //

        internal async Task<User> GetSelfUserInternalAsync(string token)
        {
            try
            {
                var response = await SendAsync("GET", "user", token);
                return response.GetBodyAsType<User>();
            }
            catch (HttpException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
            {
                return null;
            }
        }

        internal async Task<User> GetUserInternalAsync(string token, ulong userId)
        {
            try
            {
                var response = await SendAsync("GET", $"user/{userId}", token);
                return response.GetBodyAsType<User>();
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 422)
            {
                return null;
            }
        }

        internal async Task<UserCollection> GetUsersInternalAsync(string token, string[] names)
        {
            var response = await SendAsync(new GetUsersRequest(token, names));
            return response.GetBodyAsType<UserCollection>();
        }

        internal async Task<EmoteSet> GetEmotesInternalAsync(string token, ulong userId)
        {
            try
            {
                var response = await SendAsync("GET", $"users/{userId}/emotes", token);
                return response.GetBodyAsType<EmoteSet>();
            }
            catch (HttpException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
            {
                return null;
            }
        }

        internal async Task<Follow> GetFollowInternalAsync(string token, ulong userId, ulong channelId)
        {
            try
            {
                var response = await SendAsync("GET", $"users/{userId}/follows/channels/{channelId}", token);
                return response.GetBodyAsType<Follow>();
            }
            catch (HttpException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        internal async Task<FollowCollection> GetFollowsInternalAsync(string token, ulong userId, SortMode sort, bool ascending, uint limit, uint offset)
        {
            try
            {
                var response = await SendAsync(new GetFollowsRequest(token, userId, sort, ascending, limit, offset));
                return response.GetBodyAsType<FollowCollection>();
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 422)
            {
                return null;
            }
        }

        //
        // Videos
        //

        internal async Task<Video> GetVideoInternalAsync(string token, string videoId)
        {
            try
            {
                var response = await SendAsync("GET", $"videos/{videoId}", token);
                return response.GetBodyAsType<Video>();
            }
            catch (HttpException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }

    }
}
