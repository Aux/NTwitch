using System;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public class RestApiClient : IDisposable
    {
        private RestClient _client;
        private LogManager _log;

        private bool _disposed = false;

        public RestApiClient(TwitchRestConfig config, LogManager log, AuthMode type, string token)
        {
            _log = log;
            _client = new RestClient(config, type, token);
        }

        public Task<RestResponse> SendAsync(string method, string endpoint)
            => SendAsync(new RestRequest(method, endpoint));

        public async Task<RestResponse> SendAsync(RestRequest request)
        {
            await _log.DebugAsync("Rest", $"Attempting {request.Method} /{request.Endpoint}").ConfigureAwait(false);

            var message = request.GetRequest();
            var response = await _client.SendAsync(message);

            await _log.VerboseAsync("Rest", $"{request.Method} /{request.Endpoint} {response.ExecuteTime}ms").ConfigureAwait(false);
            return response;
        }

        #region Misc

        internal async Task<API.Token> ValidateTokenAsync()
        {
            try
            {
                var response = await SendAsync("GET", "");
                return response.GetBodyAsType<API.TokenCollection>().Token;
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 401)
            {
                throw new InvalidOperationException("Token is invalid.");
            }
        }

        internal async Task<API.IngestCollection> GetIngestsAsync()
        {
            try
            {
                var response = await SendAsync("GET", $"ingests");
                return response.GetBodyAsType<API.IngestCollection>();
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 401) { return null; }
        }

        #endregion
        #region Search

        internal async Task<API.ChannelCollection> SearchChannelsAsync(string query, uint limit, uint offset)
        {
            try
            {
                var response = await SendAsync(new SearchChannelsRequest(query, limit, offset));
                return response.GetBodyAsType<API.ChannelCollection>();
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 401) { return null; }
        }

        internal async Task<API.StreamCollection> SearchStreamsAsync(string query, bool? hls, uint limit, uint offset)
        {
            try
            {
                var response = await SendAsync(new SearchStreamsRequest(query, hls, limit, offset));
                return response.GetBodyAsType<API.StreamCollection>();
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 401) { return null; }
        }

        internal async Task<API.GameCollection> SearchGamesAsync(string query, bool islive)
        {
            try
            {
                var response = await SendAsync(new SearchGamesRequest(query, islive));
                return response.GetBodyAsType<API.GameCollection>();
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 401) { return null; }
        }

        #endregion
        #region Users

        internal async Task<API.User> GetCurrentUserAsync()
        {
            try
            {
                var response = await SendAsync("GET", "user");
                return response.GetBodyAsType<API.User>();
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 401) { return null; }
        }

        internal async Task<API.User> GetUserAsync(ulong id)
        {
            try
            {
                var response = await SendAsync("GET", $"users/{id}");
                return response.GetBodyAsType<API.User>();
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 422) { return null; }
        }

        internal async Task<API.UserCollection> GetUsersAsync(string[] usernames)
        {
            try
            {
                var response = await SendAsync(new GetUsersRequest(usernames));
                return response.GetBodyAsType<API.UserCollection>();
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 400) { return null; }
        }

        #endregion
        #region Channels

        internal async Task<API.Channel> GetChannelAsync(ulong id)
        {
            try
            {
                var response = await SendAsync("GET", $"channels/{id}");
                return response.GetBodyAsType<API.Channel>();
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 404) { return null; }
        }

        internal async Task<API.Subscription> GetSubscriberAsync(ulong id, ulong userId)
        {
            try
            {
                var response = await SendAsync("GET", $"channels/{id}/subscriptions/{userId}");
                return response.GetBodyAsType<API.Subscription>();
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 404) { return null; }
        }

        internal async Task<API.Channel> GetCurrentChannelAsync()
        {
            try
            {
                var response = await SendAsync("GET", "channel");
                return response.GetBodyAsType<API.Channel>();
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 401) { return null; }
        }

        internal async Task<API.Channel> ModifyChannelAsync(ulong channelId, Action<ModifyChannelParams> options)
        {
            var changes = new ModifyChannel();
            options.Invoke(changes.Parameters);

            try
            {
                var response = await SendAsync(new ModifyChannelRequest(channelId, changes));
                return response.GetBodyAsType<API.Channel>();
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 401) { return null; }
        }

        internal async Task<API.TeamCollection> GetChannelTeamsAsync(ulong id)
        {
            try
            {
                var response = await SendAsync("GET", $"channels/{id}/teams");
                return response.GetBodyAsType<API.TeamCollection>();
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 401) { return null; }
        }

        internal async Task<API.UserCollection> GetChannelEditorsAsync(ulong id)
        {
            try
            {
                var response = await SendAsync("GET", $"channels/{id}/editors");
                return response.GetBodyAsType<API.UserCollection>();
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 401) { return null; }
        }

        internal async Task<API.FollowCollection> GetChannelFollowsAsync(ulong id, bool ascending, uint limit, uint offset)
        {
            try
            {
                var response = await SendAsync("GET", $"channels/{id}/follows");
                return response.GetBodyAsType<API.FollowCollection>();
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 401) { return null; }
        }

        internal async Task<API.SubscriptionCollection> GetChannelSubscriptionsAsync(ulong id, bool ascending, uint limit, uint offset)
        {
            try
            {
                var response = await SendAsync("GET", $"channels/{id}/subscriptions");
                return response.GetBodyAsType<API.SubscriptionCollection>();
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 401) { return null; }
        }

        #endregion
        #region Streams

        internal async Task<API.StreamCollection> GetFollowedStreamsAsync(StreamType type, uint limit, uint offset)
        {
            try
            {
                var response = await SendAsync(new GetFollowedStreamsRequest(type, limit, offset));
                return response.GetBodyAsType<API.StreamCollection>();
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 401) { return null; }
        }

        internal async Task<API.StreamCollection> GetStreamAsync(ulong channelId, StreamType type)
        {
            try
            {
                var response = await SendAsync(new GetStreamRequest(channelId, type));
                return response.GetBodyAsType<API.StreamCollection>();
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 401) { return null; }
        }

        internal async Task<API.StreamCollection> GetStreamsAsync(Action<GetStreamsParams> options)
        {
            var changes = new GetStreamsParams();
            options.Invoke(changes);

            try
            {
                var response = await SendAsync(new GetStreamsRequest(changes));
                return response.GetBodyAsType<API.StreamCollection>();
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 401) { return null; }
        }

        internal async Task<API.StreamCollection> GetFeaturedStreams(uint limit, uint offset)
        {
            try
            {
                var response = await SendAsync(new GetFeaturedStreamsRequest(limit, offset));
                return response.GetBodyAsType<API.StreamCollection>();
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 401) { return null; }
        }

        internal async Task<API.Stream> GetGameSummaryAsync(string game)
        {
            try
            {
                var response = await SendAsync(new GetStreamSummaryRequest(game));
                return response.GetBodyAsType<API.Stream>();
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 401) { return null; }
        }

        #endregion
        #region Follows

        internal async Task<API.Follow> GetFollowAsync(ulong userId, ulong channelId)
        {
            try
            {
                var response = await SendAsync("GET", $"users/{userId}/follows/channels/{channelId}");
                return response.GetBodyAsType<API.Follow>();
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 404) { return null; }
        }

        internal async Task<API.FollowCollection> GetFollowsAsync(ulong userId, SortMode sort, bool ascending, uint limit, uint offset)
        {
            try
            {
                var response = await SendAsync(new GetFollowsRequest(userId, sort, ascending, limit, offset));
                return response.GetBodyAsType<API.FollowCollection>();
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 422) { return null; }
        }

        #endregion
        #region Community

        internal async Task<API.Community> GetCommunityAsync(string id, bool isname)
        {
            try
            {
                string endpoint;
                if (isname)
                    endpoint = $"communities?name={id}";
                else
                    endpoint = $"communities/{id}";
                
                var response = await SendAsync("GET", endpoint);
                return response.GetBodyAsType<API.Community>();
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 404) { return null; }
        }

        internal async Task<API.CommunityCollection> GetTopCommunitiesAsync(uint limit)
        {
            try
            {
                var response = await SendAsync("GET", $"communities/top");
                return response.GetBodyAsType<API.CommunityCollection>();
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 401) { return null; }
        }

        internal async Task<API.CommunityPermissions> GetCommunityPermissionsAsync(string communityId)
        {
            try
            {
                var response = await SendAsync("GET", $"communities/{communityId}/permissions");
                return response.GetBodyAsType<API.CommunityPermissions>();
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 404) { return null; }
        }

        internal async Task CommunityReportAsync(string id, ulong channelId)
        {
            try
            {
                var response = await SendAsync(new CommunityReportRequest(id, channelId));
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 204) { return; }
        }

        internal async Task ModifyCommunityAsync(string communityId, Action<ModifyCommunityParams> options)
        {
            var changes = new ModifyCommunityParams();
            options.Invoke(changes);

            try
            {
                var response = await SendAsync(new ModifyCommunityRequest(communityId, changes));
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 204) { return; }
        }

        internal async Task SetAvatarAsync(string communityId, string imageString)
        {
            try
            {
                var response = await SendAsync(new SetCommunityAvatarRequest(communityId, imageString));
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 204) { return; }
        }

        internal async Task RemoveAvatarAsync(string communityId)
        {
            try
            {
                var response = await SendAsync(new RemoveCommunityAvatarRequest(communityId));
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 204) { return; }
        }

        internal async Task SetCoverAsync(string communityId, string imageString)
        {
            try
            {
                var response = await SendAsync(new SetCommunityCoverRequest(communityId, imageString));
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 204) { return; }
        }

        internal async Task RemoveCoverAsync(string communityId)
        {
            try
            {
                var response = await SendAsync(new RemoveCommunityCoverRequest(communityId));
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 204) { return; }
        }

        internal async Task<API.CommunityCollection> GetCommunityModeratorsAsync(string id)
        {
            try
            {
                var response = await SendAsync("GET", $"communities/{id}/moderators");
                return response.GetBodyAsType<API.CommunityCollection>();
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 401) { return null; }
        }

        internal async Task RemoveCommunityModeratorAsync(string id, ulong userId)
        {
            try
            {
                var response = await SendAsync("PUT", $"communities/{id}/moderators/{userId}");
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 204) { return; }
        }

        internal async Task AddCommunityModeratorAsync(string id, ulong userId)
        {
            try
            {
                var response = await SendAsync("DELETE", $"communities/{id}/moderators/{userId}");
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 204) { return; }
        }

        internal async Task<API.CommunityCollection> GetCommunityBansAsync(string id, uint limit)
        {
            try
            {
                var response = await SendAsync(new GetCommunityBansRequest(id, limit));
                return response.GetBodyAsType<API.CommunityCollection>();
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 401) { return null; }
        }

        internal async Task AddCommunityBanAsync(string id, ulong userId)
        {
            try
            {
                var response = await SendAsync("PUT", $"communities/{id}/bans/{userId}");
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 204) { return; }
        }

        internal async Task RemoveCommunityBanAsync(string id, ulong userId)
        {
            try
            {
                var response = await SendAsync("DELETE", $"communities/{id}/bans/{userId}");
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 204) { return; }
        }

        internal async Task<API.CommunityCollection> GetCommunityTimeoutsAsync(string id, uint limit)
        {
            try
            {
                var response = await SendAsync(new GetCommunityTimeoutsRequest(id, limit));
                return response.GetBodyAsType<API.CommunityCollection>();
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 401) { return null; }
        }

        internal async Task AddCommunityTimeoutAsync(string id, ulong userId)
        {
            try
            {
                var response = await SendAsync("PUT", $"communities/{id}/timeouts/{userId}");
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 204) { return; }
        }

        internal async Task RemoveCommunityTimeoutAsync(string id, ulong userId)
        {
            try
            {
                var response = await SendAsync("DELETE", $"communities/{id}/timeouts/{userId}");
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 204) { return; }
        }

        #endregion
        #region Videos

        internal async Task<API.Video> GetVideoAsync(string id)
        {
            try
            {
                var response = await SendAsync("GET", $"videos/{id}");
                return response.GetBodyAsType<API.Video>();
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 404) { return null; }
        }

        #endregion
        #region Emotes

        internal async Task<API.EmoteSet> GetEmotesAsync(ulong id)
        {
            try
            {
                var response = await SendAsync("GET", $"users/{id}/emotes");
                return response.GetBodyAsType<API.EmoteSet>();
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 404) { return null; }
        }

        #endregion
        #region Teams

        internal async Task<API.Team> GetTeamAsync(string name)
        {
            try
            {
                var response = await SendAsync("GET", $"teams/{name}");
                return response.GetBodyAsType<API.Team>();
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 404) { return null; }
        }

        internal async Task<API.TeamCollection> GetTeamsAsync(uint limit, uint offset)
        {
            try
            {
                var response = await SendAsync("GET", $"teams");
                return response.GetBodyAsType<API.TeamCollection>();
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 404) { return null; }
        }

        #endregion
        #region Chat

        internal async Task<API.CheerCollection> GetCheersAsync(ulong? id)
        {
            try
            {
                var response = await SendAsync(new GetCheersRequest(id));
                return response.GetBodyAsType<API.CheerCollection>();
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 401) { return null; }
        }

        internal async Task<API.ChatBadges> GetChatBadgesAsync(ulong id)
        {
            try
            {
                var response = await SendAsync("GET", $"chat/{id}/badges");
                return response.GetBodyAsType<API.ChatBadges>();
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 401) { return null; }
        }

        #endregion

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _client.Dispose();
                }
                
                _disposed = true;
            }
        }
        
        public void Dispose()
        {
            Dispose(true);
        }
    }
}
