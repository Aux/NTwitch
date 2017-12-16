using Newtonsoft.Json;
using NTwitch.Helix.Queue;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NTwitch.Helix.Rest.API
{
    internal class TwitchRestApiClient : IDisposable
    {
        public event Func<string, string, double, Task> SentRequest { add { _sentRequestEvent.Add(value); } remove { _sentRequestEvent.Remove(value); } }
        private readonly AsyncEvent<Func<string, string, double, Task>> _sentRequestEvent = new AsyncEvent<Func<string, string, double, Task>>();

        protected readonly JsonSerializer _serializer;
        protected readonly SemaphoreSlim _stateLock;
        private readonly RestClientProvider _restClientProvider;

        protected bool _isDisposed;
        private CancellationTokenSource _loginCancelToken;

        public RetryMode DefaultRetryMode { get; }
        public string UserAgent { get; }
        public string ClientId { get; }
        internal RequestQueue RequestQueue { get; }

        public LoginState LoginState { get; private set; }
        internal string AuthToken { get; private set; }
        internal IRestClient RestClient { get; private set; }
        internal ulong? CurrentUserId { get; set; }

        public TwitchRestApiClient(RestClientProvider restClientProvider, string userAgent, RetryMode defaultRetryMode = RetryMode.AlwaysRetry,
            JsonSerializer serializer = null)
        {
            _restClientProvider = restClientProvider;
            UserAgent = userAgent;
            DefaultRetryMode = defaultRetryMode;
            _serializer = serializer ?? new JsonSerializer { DateFormatString = "yyyy-MM-ddTHH:mm:ssZ" };

            RequestQueue = new RequestQueue();
            _stateLock = new SemaphoreSlim(1, 1);

            SetBaseUrl(TwitchConfig.APIUrl);
        }
        internal void SetBaseUrl(string baseUrl)
        {
            RestClient = _restClientProvider(baseUrl);
            RestClient.SetHeader("accept", $"application/vnd.twitchtv.v{TwitchConfig.APIVersion}+json");
            RestClient.SetHeader("authorization", $"Bearer: {AuthToken}");
            if (ClientId != null)
                RestClient.SetHeader("client-id", ClientId);
        }
        internal virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    _loginCancelToken?.Dispose();
                    (RestClient as IDisposable)?.Dispose();
                }
                _isDisposed = true;
            }
        }
        public void Dispose() => Dispose(true);

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
                await RequestQueue.SetCancelTokenAsync(_loginCancelToken.Token).ConfigureAwait(false);
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

            await DisconnectInternalAsync().ConfigureAwait(false);
            await RequestQueue.ClearAsync().ConfigureAwait(false);

            await RequestQueue.SetCancelTokenAsync(CancellationToken.None).ConfigureAwait(false);
            RestClient.SetCancelToken(CancellationToken.None);

            CurrentUserId = null;
            LoginState = LoginState.LoggedOut;
        }

        internal virtual Task ConnectInternalAsync() => Task.Delay(0);
        internal virtual Task DisconnectInternalAsync() => Task.Delay(0);

        //Core
        internal Task SendAsync(string method, Expression<Func<string>> endpointExpr,
             ClientBucketType clientBucket = ClientBucketType.Unbucketed, RequestOptions options = null, [CallerMemberName] string funcName = null)
            => SendAsync(method, GetEndpoint(endpointExpr), clientBucket, options);
        public async Task SendAsync(string method, string endpoint,
            ClientBucketType clientBucket = ClientBucketType.Unbucketed, RequestOptions options = null)
        {
            options = options ?? new RequestOptions();
            options.HeaderOnly = true;
            options.BucketId = ClientBucket.Get(clientBucket).Id;

            var request = new RestRequest(RestClient, method, endpoint, options);
            await SendInternalAsync(method, endpoint, request).ConfigureAwait(false);
        }

        internal Task SendJsonAsync(string method, Expression<Func<string>> endpointExpr, object payload,
             ClientBucketType clientBucket = ClientBucketType.Unbucketed, RequestOptions options = null, [CallerMemberName] string funcName = null)
            => SendJsonAsync(method, GetEndpoint(endpointExpr), payload, clientBucket, options);
        public async Task SendJsonAsync(string method, string endpoint, object payload,
            ClientBucketType clientBucket = ClientBucketType.Unbucketed, RequestOptions options = null)
        {
            options = options ?? new RequestOptions();
            options.HeaderOnly = true;
            options.BucketId = ClientBucket.Get(clientBucket).Id;

            string json = payload != null ? SerializeJson(payload) : null;
            var request = new JsonRestRequest(RestClient, method, endpoint, json, options);
            await SendInternalAsync(method, endpoint, request).ConfigureAwait(false);
        }

        internal Task<TResponse> SendAsync<TResponse>(string method, Expression<Func<string>> endpointExpr,
             ClientBucketType clientBucket = ClientBucketType.Unbucketed, RequestOptions options = null, [CallerMemberName] string funcName = null) where TResponse : class
            => SendAsync<TResponse>(method, GetEndpoint(endpointExpr), clientBucket, options);
        public async Task<TResponse> SendAsync<TResponse>(string method, string endpoint,
            ClientBucketType clientBucket = ClientBucketType.Unbucketed, RequestOptions options = null) where TResponse : class
        {
            options = options ?? new RequestOptions();
            options.BucketId = ClientBucket.Get(clientBucket).Id;

            var request = new RestRequest(RestClient, method, endpoint, options);
            return DeserializeJson<TResponse>(await SendInternalAsync(method, endpoint, request).ConfigureAwait(false));
        }

        internal Task<TResponse> SendJsonAsync<TResponse>(string method, Expression<Func<string>> endpointExpr, object payload,
             ClientBucketType clientBucket = ClientBucketType.Unbucketed, RequestOptions options = null, [CallerMemberName] string funcName = null) where TResponse : class
            => SendJsonAsync<TResponse>(method, GetEndpoint(endpointExpr), payload, clientBucket, options);
        public async Task<TResponse> SendJsonAsync<TResponse>(string method, string endpoint, object payload,
            ClientBucketType clientBucket = ClientBucketType.Unbucketed, RequestOptions options = null) where TResponse : class
        {
            options = options ?? new RequestOptions();
            options.BucketId = ClientBucket.Get(clientBucket).Id;

            string json = payload != null ? SerializeJson(payload) : null;
            var request = new JsonRestRequest(RestClient, method, endpoint, json, options);
            return DeserializeJson<TResponse>(await SendInternalAsync(method, endpoint, request).ConfigureAwait(false));
        }

        private async Task<Stream> SendInternalAsync(string method, string endpoint, RestRequest request)
        {
            if (!request.Options.IgnoreState)
                CheckState();
            if (request.Options.RetryMode == null)
                request.Options.RetryMode = DefaultRetryMode;

            var stopwatch = Stopwatch.StartNew();
            var responseStream = await RequestQueue.SendAsync(request).ConfigureAwait(false);
            stopwatch.Stop();

            double milliseconds = ToMilliseconds(stopwatch);
            await _sentRequestEvent.InvokeAsync(method, endpoint, milliseconds).ConfigureAwait(false);

            return responseStream;
        }

        // Requests



        //Helpers
        protected void CheckState(bool validateClientId = false)
        {
            if (LoginState != LoginState.LoggedIn && string.IsNullOrWhiteSpace(ClientId))
            {
                if (validateClientId && string.IsNullOrWhiteSpace(ClientId))
                    throw new InvalidOperationException("ClientId must be specified in the client config for unauthenticated requests.");

                throw new InvalidOperationException("Client is not logged in.");
            }
        }
        // No such thing as token info in helix api :^)
        //protected virtual void CheckScopes(IEnumerable<string> requiredScopes)
        //{
        //    if (TokenInfo == null) return;
        //    if (!requiredScopes.Intersect(TokenInfo.Authorization.Scopes).Any())
        //        throw new MissingScopeException(requiredScopes);
        //}

        protected static double ToMilliseconds(Stopwatch stopwatch) => Math.Round(stopwatch.ElapsedTicks / (double)Stopwatch.Frequency * 1000.0, 2);
        protected string SerializeJson(object value)
        {
            var sb = new StringBuilder(256);
            using (TextWriter text = new StringWriter(sb, CultureInfo.InvariantCulture))
            using (JsonWriter writer = new JsonTextWriter(text))
                _serializer.Serialize(writer, value);
            return sb.ToString();
        }
        protected T DeserializeJson<T>(Stream jsonStream)
        {
            using (TextReader text = new StreamReader(jsonStream))
            using (JsonReader reader = new JsonTextReader(text))
                return _serializer.Deserialize<T>(reader);
        }

        private static string GetEndpoint(Expression<Func<string>> endpointExpr)
        {
            return endpointExpr.Compile()();
        }
        private static string GetFieldName(Expression expr)
        {
            if (expr.NodeType == ExpressionType.Convert)
                expr = (expr as UnaryExpression).Operand;

            if (expr.NodeType != ExpressionType.MemberAccess)
                throw new InvalidOperationException("Unsupported expression");

            return (expr as MemberExpression).Member.Name;
        }
    }
}
