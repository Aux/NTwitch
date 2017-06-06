using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NTwitch.Rest.API
{
    internal sealed class DefaultRestClient : IRestClient, IDisposable
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl;
        private CancellationToken _cancelToken;
        private bool _disposed;

        public DefaultRestClient(string baseUrl)
        {
            _baseUrl = baseUrl;
            _cancelToken = CancellationToken.None;

            _client = new HttpClient(new HttpClientHandler
            {
                UseCookies = false,
                UseProxy = false
            });
        }
        
        void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                    _client.Dispose();
                _disposed = true;
            }
        }

        public void Dispose() => Dispose(true);

        public void SetHeader(string key, string value)
        {
            _client.DefaultRequestHeaders.Remove(key);
            if (value != null)
                _client.DefaultRequestHeaders.Add(key, value);
        }

        public void SetCancelToken(CancellationToken cancelToken)
        {
            _cancelToken = cancelToken;
        }

        public async Task<RestResponse> SendAsync(string method, string endpoint, CancellationToken cancelToken, bool headerOnly = false)
        {
            string uri = Path.Combine(_baseUrl, endpoint);
            using (var request = new HttpRequestMessage(new HttpMethod(method), uri))
                return await SendInternalAsync(request, cancelToken, headerOnly).ConfigureAwait(false);
        }

        public async Task<RestResponse> SendAsync(string method, string endpoint, string json, CancellationToken cancelToken, bool headerOnly = false)
        {
            string uri = Path.Combine(_baseUrl, endpoint);
            using (var request = new HttpRequestMessage(new HttpMethod(method), uri))
            {
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                return await SendInternalAsync(request, cancelToken, headerOnly).ConfigureAwait(false);
            }
        }

        private async Task<RestResponse> SendInternalAsync(HttpRequestMessage request, CancellationToken cancelToken, bool headerOnly)
        {
            cancelToken = CancellationTokenSource.CreateLinkedTokenSource(_cancelToken, cancelToken).Token;
            var response = await _client.SendAsync(request, cancelToken).ConfigureAwait(false);
            
            var headers = response.Headers.ToDictionary(x => x.Key, x => x.Value.FirstOrDefault(), StringComparer.OrdinalIgnoreCase);
            var stream = !headerOnly ? await response.Content.ReadAsStreamAsync().ConfigureAwait(false) : null;

            return new RestResponse(response.StatusCode, headers, stream);
        }
    }
}
