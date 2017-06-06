using Newtonsoft.Json;
using NTwitch.Rest.Queue;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NTwitch.Rest.API
{
    internal class TwitchRestApiClient : IDisposable
    {
        public event Func<string, string, double, Task> SentRequest { add { _sentRequestEvent.Add(value); } remove { _sentRequestEvent.Remove(value); } }
        private readonly AsyncEvent<Func<string, string, double, Task>> _sentRequestEvent = new AsyncEvent<Func<string, string, double, Task>>();

        protected readonly JsonSerializer _serializer;
        protected readonly SemaphoreSlim _stateLock;
        private readonly RestClientProvider RestClientProvider;

        protected bool _disposed;
        private CancellationTokenSource _loginCancelToken;

        //public RetryMode DefaultRetryMode { get; }
        public string UserAgent { get; }
        public string ClientId { get; }

        public LoginState LoginState { get; private set; }
        internal IRestClient RestClient { get; private set; }
        internal string AuthToken { get; private set; }
        internal ulong? CurrentUserId { get; set; }

        public TwitchRestApiClient(RestClientProvider restClientProvider, string clientId, string userAgent, JsonSerializer serializer = null)
        {
            RestClientProvider = restClientProvider;
            UserAgent = userAgent;
            ClientId = clientId;
            _serializer = serializer ?? new JsonSerializer { DateFormatString = "yyyy-MM-ddTHH:mm:ssZ" };
            //DefaultRetryMode = defaultRetryMode;
            
            _stateLock = new SemaphoreSlim(1, 1);

            SetBaseUrl(TwitchConfig.APIUrl);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _loginCancelToken?.Dispose();
                    (RestClient as IDisposable)?.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose() => Dispose(true);

        internal void SetBaseUrl(string baseUrl)
        {
            RestClient = RestClientProvider(baseUrl);
            RestClient.SetHeader("Accept", $"application/vnd.twitchtv.v{TwitchConfig.APIVersion}+json");
            RestClient.SetHeader("UserAgent", UserAgent);
            if (ClientId != null)
                RestClient.SetHeader("Client-ID", ClientId);
        }

        public async Task LoginAsync(string token, RequestOptions options = null)
        {
            await _stateLock.WaitAsync().ConfigureAwait(false);
            try
            {
                await LoginInternalAsync(token, options).ConfigureAwait(false);
            }
            finally { _stateLock.Release(); }
        }

        private async Task LoginInternalAsync(string token, RequestOptions options = null)
        {
            if (LoginState != LoginState.LoggedOut)
                await LogoutInternalAsync().ConfigureAwait(false);
            LoginState = LoginState.LoggingIn;

            try
            {
                _loginCancelToken = new CancellationTokenSource();
                AuthToken = null;

                RestClient.SetHeader("Authorization", $"OAuth {token}");
                RestClient.SetCancelToken(_loginCancelToken.Token);

                AuthToken = token;
                LoginState = LoginState.LoggedIn;
            }
            catch (Exception)
            {
                await LogoutInternalAsync().ConfigureAwait(false);
                throw;
            }
        }

        public async Task LogoutAsync()
        {
            await _stateLock.WaitAsync().ConfigureAwait(false);
            try
            {
                await LogoutInternalAsync().ConfigureAwait(false);
            }
            finally { _stateLock.Release(); }
        }

        private async Task LogoutInternalAsync()
        {
            if (LoginState == LoginState.LoggedOut) return;
            LoginState = LoginState.LoggingOut;

            try { _loginCancelToken?.Cancel(false); }
            catch { }

            RestClient.SetHeader("Authorization", null);
            await DisconnectInternalAsync().ConfigureAwait(false);

            CurrentUserId = null;
            LoginState = LoginState.LoggedOut;
        }

        internal virtual Task ConnectInternalAsync() => Task.Delay(0);
        internal virtual Task DisconnectInternalAsync() => Task.Delay(0);
        
        public async Task SendAsync(string method, string endpoint, RequestOptions options = null)
        {
            options = options ?? new RequestOptions();

            var request = new RestRequest(RestClient, method, endpoint, options);
            await SendInternalAsync(method, endpoint, request).ConfigureAwait(false);
        }

        public async Task<TResponse> SendAsync<TResponse>(string method, string endpoint, RequestOptions options = null)
        {
            options = options ?? new RequestOptions();

            var request = new RestRequest(RestClient, method, endpoint, options);
            return DeserializeJson<TResponse>(await SendInternalAsync(method, endpoint, request).ConfigureAwait(false));
        }

        public async Task<TResponse> SendJsonAsync<TResponse>(string method, string endpoint, object payload, RequestOptions options = null)
        {
            options = options ?? new RequestOptions();

            var json = payload != null ? SerializeJson(payload) : null;
            var request = new JsonRestRequest(RestClient, method, endpoint, json, options);
            return DeserializeJson<TResponse>(await SendInternalAsync(method, endpoint, request).ConfigureAwait(false));
        }

        private async Task<System.IO.Stream> SendInternalAsync(string method, string endpoint, RestRequest request)
        {
            if (!request.Options.IgnoreState)
                CheckState();
            //if (request.Options.RetryMode == null)
            //    request.Options.RetryMode = DefaultRetryMode;

            var stopwatch = Stopwatch.StartNew();
            var response = await request.SendAsync().ConfigureAwait(false);
            stopwatch.Stop();

            double milliseconds = ToMilliseconds(stopwatch);
            await _sentRequestEvent.InvokeAsync(method, endpoint, milliseconds).ConfigureAwait(false);

            return response.Body;
        }

        protected void CheckState()
        {
            if (LoginState != LoginState.LoggedIn)
                throw new InvalidOperationException("Client is not logged in.");
        }

        protected string SerializeJson(object value)
        {
            var sb = new StringBuilder(256);
            using (TextWriter text = new StringWriter(sb, CultureInfo.InvariantCulture))
            using (JsonWriter writer = new JsonTextWriter(text))
                _serializer.Serialize(writer, value);
            return sb.ToString();
        }

        protected T DeserializeJson<T>(System.IO.Stream jsonStream)
        {
            using (TextReader text = new StreamReader(jsonStream))
            using (JsonReader reader = new JsonTextReader(text))
                return _serializer.Deserialize<T>(reader);
        }

        protected static double ToMilliseconds(Stopwatch stopwatch) 
            => Math.Round((double)stopwatch.ElapsedTicks / Stopwatch.Frequency * 1000.0, 2);

        // Auth
        public async Task<TokenCollection> ValidateTokenAsync(RequestOptions options = null)
        {
            options = RequestOptions.CreateOrClone(options);
            return await SendAsync<TokenCollection>("GET", "", options).ConfigureAwait(false);
        }

        // Users
        internal async Task<User> GetMyUserAsync(RequestOptions options)
        {
            options = RequestOptions.CreateOrClone(options);
            return await SendAsync<User>("GET", "user", options);
        }

    }
}
