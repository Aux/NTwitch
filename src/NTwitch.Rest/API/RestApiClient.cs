using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public class RestApiClient : IDisposable
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
                http.DefaultRequestHeaders.Add("Accept", "application/vnd.twitchtv.v5+json");
                http.DefaultRequestHeaders.Add("Client-ID", _clientid);

                if (string.IsNullOrWhiteSpace(_token))
                    http.DefaultRequestHeaders.Add("Authorization", "OAuth " + _token);

                _http = http;
            }
        }

        private HttpRequestMessage BuildRequest(string method, string endpoint, Dictionary<string, string> parameters, object payload)
        {
            EnsureHttpClientCreated();
            if (parameters != null)
                endpoint += ParseQuery(parameters);
            var builder = new UriBuilder(endpoint);
            
            var request = new HttpRequestMessage(new HttpMethod(method), endpoint);

            if (payload != null)
                request.Content = new StringContent(JsonConvert.SerializeObject(payload));
            
            return request;
        }

        public async Task SendAsync(string method, string endpoint, Dictionary<string, string> parameters = null, object payload = null)
        {
            var start = DateTime.UtcNow;
            var request = BuildRequest(method, endpoint, parameters, payload);
            var response = await _http.SendAsync(request);
            
            response.EnsureSuccessStatusCode();
            double total = (DateTime.UtcNow - start).TotalMilliseconds;
            await _log.DebugAsync("Rest", endpoint + " " + total.ToString() + "ms");
        }
        
        public async Task<string> GetJsonAsync(string method, string endpoint, Dictionary<string, string> parameters = null, object payload = null)
        {
            var start = DateTime.UtcNow;
            var request = BuildRequest(method, endpoint, parameters, payload);
            var response = await _http.SendAsync(request);
            
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();

            double total = (DateTime.UtcNow - start).TotalMilliseconds;
            await _log.DebugAsync("Rest", endpoint + " " + total.ToString() + "ms");
            return content;
        }

        internal async Task<TwitchValidation> LoginAsync(string token)
        {
            _token = token;
            await Task.Delay(1);
            return null;
        }

        internal string ParseQuery(Dictionary<string, string> parameters)
        {
            var builder = new StringBuilder();
            foreach (var p in parameters)
            {
                string name = p.Key.ToLower();
                string value = p.Value;

                if (string.IsNullOrWhiteSpace(value))
                    continue;

                if (builder.Length < 1)
                    builder.AppendFormat("?{0}={1}", name, value);
                else
                    builder.AppendFormat("&{0}={1}", name, value);
            }

            return builder.ToString();
        }

        public void Dispose()
        {
            _http.Dispose();
        }
    }
}
