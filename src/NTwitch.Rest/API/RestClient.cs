using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public class RestClient : IDisposable
    {
        private LogManager _log;
        private HttpClient _client;

        private string _host;
        private string _clientid;
        private string _token;
        private bool _disposed;

        internal RestClient(LogManager log, string host, string clientid, string token)
        {
            _log = log;
            _host = host;
            _clientid = clientid;
            _token = token;
        }
        
        public async Task<string> SendAsync(string method, string endpoint, RequestOptions options = null)
        {
            var stopwatch = Stopwatch.StartNew();
            if (options == null)
                options = new RequestOptions();

            var request = await BuildRequest(method, endpoint, options);
            var response = await _client.SendAsync(request);

            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();

            await _log.DebugAsync("Rest", endpoint + " " + stopwatch.ElapsedMilliseconds + " ms");
            stopwatch.Stop();

            return content;
        }

        internal Task EnsureHttpClientCreated(RequestOptions options)
        {
            if (_client == null)
            {
                var http = new HttpClient();

                http.BaseAddress = new Uri(_host);
                http.DefaultRequestHeaders.Add("Accept", "application/vnd.twitchtv.v" + options.ApiVersion + "+json");
                http.DefaultRequestHeaders.Add("Client-ID", _clientid);

                if (!string.IsNullOrWhiteSpace(_token))
                    http.DefaultRequestHeaders.Add("Authorization", "OAuth " + _token);

                _client = http;
            }
            return Task.CompletedTask;
        }

        internal async Task<HttpRequestMessage> BuildRequest(string method, string endpoint, RequestOptions options = null)
        {
            await EnsureHttpClientCreated(options);

            if (options.Parameters.Count > 0)
                endpoint += await ParseQuery(options.Parameters);

            var request = new HttpRequestMessage(new HttpMethod(method), endpoint);

            if (options.Payload != null)
            {
                string content = JsonConvert.SerializeObject(options.Payload);
                request.Content = new StringContent(content);
            }

            return request;
        }

        internal Task<string> ParseQuery(Dictionary<string, object> parameters)
        {
            var builder = new StringBuilder();
            foreach (var p in parameters)
            {
                string name = p.Key.ToLower();
                string value = p.Value.ToString();

                if (string.IsNullOrWhiteSpace(value))
                    continue;

                if (builder.Length < 1)
                    builder.AppendFormat("?{0}={1}", name, value);
                else
                    builder.AppendFormat("&{0}={1}", name, value);
            }

            return Task.FromResult(builder.ToString());
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _client.Dispose();
                    _client = null;
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
