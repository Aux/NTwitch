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

        public RestApiClient(string host, TokenType type, string token)
        {
            _client = new RestClient(host, type, token);
        }
        
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
                var response = await SendAsync(new GetCurrentUserRequest());
                return response.GetBodyAsType<API.SelfUser>();
            }
            catch (HttpException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized) { return null; }
        }

        internal async Task<API.User> GetUserAsync(ulong id)
        {
            try
            {
                var response = await SendAsync(new GetUserRequest(id));
                return response.GetBodyAsType<API.SelfUser>();
            }
            catch (HttpException ex) when ((int)ex.StatusCode == 422) { return null; }
        }

        #endregion

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                _disposed = true;
            }
        }
        
        public void Dispose()
        {
            Dispose(true);
        }
    }
}
