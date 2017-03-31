using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace NTwitch.Rest.API
{
    internal class RestClient
    {
        public string RestHost => _host;

        private HttpClient _client = null;
        private string _host;
        private string _clientId;
        private bool _disposed = false;

        public RestClient(string host, string clientId = null)
        {
            _host = host;
            _clientId = clientId;
        }

        public async Task<RestResponse> SendAsync(HttpRequestMessage message)
        {
            if (_disposed)
                throw new InvalidOperationException("Client is disposed");

            var timer = new Stopwatch();
            timer.Start();

            EnsureClientExists();

            var reply = await _client.SendAsync(message);

            try
            {
                reply.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                throw new HttpException(reply.StatusCode, ex);
            }

            timer.Stop();
            var code = reply.StatusCode;
            var body = await reply.Content.ReadAsStringAsync();

            return new RestResponse(code, body, timer.ElapsedMilliseconds);
        }

        private void EnsureClientExists()
        {
            if (_client == null)
            {
                var client = new HttpClient();

                client.BaseAddress = new Uri(_host);
                client.DefaultRequestHeaders.Add("Accept", "application/vnd.twitchtv.v5+json");
                client.DefaultRequestHeaders.Add("User-Agent", TwitchConfig.UserAgent);

                if (!string.IsNullOrWhiteSpace(_clientId))
                    client.DefaultRequestHeaders.Add("Client-ID", _clientId);

                _client = client;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _client.Dispose();
                }

                _client = null;
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
