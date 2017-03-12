using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public class RestApiClient : IApiClient, IDisposable
    {
        private RestClient _client;
        
        private bool _disposed = false;

        public RestApiClient(string host, TokenType type, string token)
        {
            _client = new RestClient(host, type, token);
        }
        
        public Task LoginAsync()
        {
            throw new NotImplementedException();
        }

        public async Task SendAsync(RestRequest request)
        {
            var endpoint = string.Format(request.Endpoint, request.GetParameters());
            var message = new HttpRequestMessage(new HttpMethod(request.Method), endpoint);
            
            if (!string.IsNullOrWhiteSpace(request.JsonBody))
            {
                string content = JsonConvert.SerializeObject(request.JsonBody);
                message.Content = new StringContent(content);
            }

            var response = await _client.SendAsync(message);
            if (response.StatusCode != HttpStatusCode.OK)
                throw new HttpRequestException($"Request failed with {(int)response.StatusCode}: {response.StatusCode}");
        }

        public Task<T> SendAsync<T>(RestRequest request)
        {
            throw new NotImplementedException();
        }
        
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
