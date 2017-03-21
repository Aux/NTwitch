using Newtonsoft.Json;
using NTwitch.Rest.Requests;
using System;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public class RestApiClient : IDisposable
    {
        private RestClient _client;
        private LogManager _log;

        private bool _disposed = false;

        public RestApiClient(TwitchRestConfig config, LogManager log, TokenType type, string token)
        {
            _log = log;
            _client = new RestClient(config, type, token);
        }

        public Task<RestResponse> SendAsync(string method, string endpoint)
            => SendAsync(new RestRequest(method, endpoint));

        public async Task<RestResponse> SendAsync(RestRequest request)
        {
            var endpoint = request.Endpoint + request.GetParameterString();
            var message = new HttpRequestMessage(new HttpMethod(request.Method), endpoint);

            await _log.DebugAsync("Rest", $"Attempting {request.Method} /{endpoint}").ConfigureAwait(false);
            if (!string.IsNullOrWhiteSpace(request.JsonBody))
            {
                string content = JsonConvert.SerializeObject(request.JsonBody);
                message.Content = new StringContent(content);
            }

            var response = await _client.SendAsync(message);
            await _log.VerboseAsync("Rest", $"{request.Method} /{request.Endpoint} {response.ExecuteTime}ms").ConfigureAwait(false);
            return response;
        }

        #region Authorization

        internal async Task<API.Token> ValidateTokenAsync()
        {
            var response = await SendAsync(new ValidateTokenRequest());
            if (response.StatusCode != HttpStatusCode.OK)
                throw new AuthenticationException("Token is invalid.");
            return response.GetBodyAsType<API.PreToken>().Token;
        }

        #endregion
        #region Users

        internal async Task<API.SelfUser> GetCurrentUserAsync()
        {
            try
            {
                var response = await SendAsync("GET", "user");
                return response.GetBodyAsType<API.SelfUser>();
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 401) { return null; }
        }

        internal async Task<API.User> GetUserAsync(ulong id)
        {
            try
            {
                var response = await SendAsync(new GetUserRequest(id));
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

        internal async Task<API.SelfChannel> GetCurrentChannelAsync()
        {
            try
            {
                var response = await SendAsync("GET", "channel");
                return response.GetBodyAsType<API.SelfChannel>();
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 401) { return null; }
        }
        
        internal async Task<API.PreCheer> GetCheersAsync(ulong? id)
        {
            try
            {
                var response = await SendAsync(new GetCheersRequest(id));
                return response.GetBodyAsType<API.PreCheer>();
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 401) { return null; }
        }

        internal async Task<API.PreCheer> ModifyChannelAsync(ulong channelId, ModifyChannelParams changes)
        {
            try
            {
                var response = await SendAsync(new ModifyChannelRequest(channelId, changes));
                return response.GetBodyAsType<API.PreCheer>();
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 401) { return null; }
        }

        #endregion
        #region Follows

        internal async Task<API.ChannelFollow> GetFollowAsync(ulong userId, ulong channelId)
        {
            try
            {
                var response = await SendAsync("GET", $"users/{userId}/follows/channels/{channelId}");
                return response.GetBodyAsType<API.ChannelFollow>();
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 404) { return null; }
        }

        internal async Task<API.FollowCollection<API.ChannelFollow>> GetFollowsAsync(ulong userId, SortMode sort, bool ascending, int limit, int offset)
        {
            try
            {
                var response = await SendAsync(new GetFollowsRequest(userId, sort, ascending, limit, offset));
                return response.GetBodyAsType<API.FollowCollection<API.ChannelFollow>>();
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
