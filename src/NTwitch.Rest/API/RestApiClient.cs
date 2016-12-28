using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    internal class RestApiClient : IDisposable
    {
        private LogManager _log;
        private HttpClient _http;
        private string _baseurl;
        private string _clientid;
        private string _token;

        internal RestApiClient(LogManager log, string baseurl, string clientid, string token = null)
        {
            _log = log;
            _clientid = clientid;
            _baseurl = baseurl;
            _token = token;
        }

        private void EnsureHttpClientCreated()
        {
            if (_http == null)
            {
                var http = new HttpClient();

                http.BaseAddress = new Uri(_baseurl);
                http.DefaultRequestHeaders.Add("Accept", "application/vnd.twitchtv.v2+json");
                http.DefaultRequestHeaders.Add("Client-ID", _clientid);

                if (string.IsNullOrWhiteSpace(_token))
                    http.DefaultRequestHeaders.Add("Authorization", "OAuth " + _token);

                _http = http;
            }
        }

        private HttpRequestMessage BuildRequest(string method, string endpoint, TwitchPageOptions options, object payload)
        {
            EnsureHttpClientCreated();
            endpoint += options?.ToString();
            var request = new HttpRequestMessage(new HttpMethod(method), endpoint);

            if (payload != null)
                request.Content = new StringContent(JsonConvert.SerializeObject(payload));

            _log.InfoAsync("RestApiClient", method + " " + endpoint + options?.ToString());
            return request;
        }

        internal async Task SendAsync(string method, string endpoint, TwitchPageOptions options = null, object payload = null)
        {
            var request = BuildRequest(method, endpoint, options, payload);
            var response = await _http.SendAsync(request);
            
            response.EnsureSuccessStatusCode();
        }

        internal async Task<T> SendAsync<T>(string method, string endpoint, TwitchPageOptions options = null, object payload = null)
        {
            var request = BuildRequest(method, endpoint, options, payload);
            var response = await _http.SendAsync(request);

            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();

            await _log.DebugAsync("RestApiClient", "Return: " + content);
            return JsonConvert.DeserializeObject<T>(content);
        }

        internal async Task<TwitchValidation> LoginAsync(string token)
        {
            var validation = await SendAsync<TwitchValidation>("POST", "oauth2/token");

            if (!validation.IsValid)
                throw new HttpRequestException("Token is not valid.");
            else
                return validation;
        }

        public void Dispose()
        {
            _http.Dispose();
        }
    }
}
