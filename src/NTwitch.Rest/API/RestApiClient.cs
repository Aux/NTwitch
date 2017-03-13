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
        private bool _disposed = false;

        public RestApiClient(TwitchRestConfig config, TokenType type, string token)
        {
            _client = new RestClient(config, type, token);
        }

        private Task<RestResponse> SendAsync(string method, string endpoint)
            => SendAsync(new RestRequest(method, endpoint));

        public async Task<RestResponse> SendAsync(RestRequest request)
        {
            var endpoint = string.Format(request.Endpoint, request.GetParameters());
            var message = new HttpRequestMessage(new HttpMethod(request.Method), endpoint);

            if (!string.IsNullOrWhiteSpace(request.JsonBody))
            {
                string content = JsonConvert.SerializeObject(request.JsonBody);
                message.Content = new StringContent(content);
            }

            var response = await _client.SendAsync(message);
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
                var response = await SendAsync(new GetChannelRequest(id));
                return response.GetBodyAsType<API.Channel>();
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 422) { return null; }
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
