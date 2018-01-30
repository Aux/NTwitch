using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NTwitch.Queue;
using System.IO;
using System.Runtime.CompilerServices;
using System.Net;

namespace NTwitch.Rest.API
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
        internal RestTokenInfo TokenInfo { get; private set; }

        public TwitchRestApiClient(RestClientProvider restClientProvider, string clientId, string userAgent, RetryMode defaultRetryMode = RetryMode.AlwaysRetry,
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
            RestClient.SetHeader("useragent", UserAgent);
            if (AuthToken != null)
                RestClient.SetHeader("authorization", AuthToken);
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

        // Authorization
        public async Task<TokenData> GetTokenAsync(RequestOptions options = null)
        {
            options = RequestOptions.CreateOrClone(options);
            return await SendAsync<TokenData>("GET", "", options: options).ConfigureAwait(false);
        }

        // Channels
        public async Task<Channel> GetMyChannelAsync(RequestOptions options = null)
        {
            options = RequestOptions.CreateOrClone(options);
            options.AddRequiredScopes("channel_read");
            return await SendAsync<Channel>("GET", () => "channel", options: options).ConfigureAwait(false);
        }
        public async Task<Channel> GetChannelAsync(ulong channelId, RequestOptions options = null)
        {
            options = RequestOptions.CreateOrClone(options);
            options.AllowUnauthenticated = true;

            try
            {
                return await SendAsync<Channel>("GET", () => $"channels/{channelId}", options: options).ConfigureAwait(false);
            }
            catch (HttpException ex) when (ex.HttpCode == (HttpStatusCode)422) { return null; }
        }
        public async Task<Channel> ModifyChannelAsync(ulong channelId, ModifyChannelParams args, RequestOptions options)
        {
            options = RequestOptions.CreateOrClone(options);
            options.AddRequiredScopes("channel_editor");
            return await SendJsonAsync<Channel>("PUT", () => $"channels/{channelId}", new { channel = args }, options: options).ConfigureAwait(false);
        }
        public async Task<UserData> GetChannelEditorsAsync(ulong channelId, RequestOptions options)
        {
            options = RequestOptions.CreateOrClone(options);
            options.AddRequiredScopes("channel_read");
            return await SendAsync<UserData>("GET", () => $"channels/{channelId}/editors", options: options).ConfigureAwait(false);
        }
        public async Task<FollowData> GetChannelFollowersAsync(ulong channelId, GetChannelFollowersParams args, RequestOptions options = null) // Supports Paging
        {
            options = RequestOptions.CreateOrClone(options);
            options.AllowUnauthenticated = true;

            var query = new Dictionary<string, object>
            {
                { "direction", args.IsAscending.GetValueOrDefault() ? "asc" : "desc" },
                { "limit", args.Limit.GetValueOrDefault() },
                { "offset", args.Offset.GetValueOrDefault() }
            };

            return await SendAsync<FollowData>("GET", () => $"channels/{channelId}/follows{GetQueryParameters(query)}", options: options).ConfigureAwait(false);
        }
        public async Task<VideoData> GetChannelVideosAsync(ulong channelId, RequestOptions options) // Supports Paging
        {
            options = RequestOptions.CreateOrClone(options);
            options.AllowUnauthenticated = true;

            return await SendAsync<VideoData>("GET", () => $"channels/{channelId}/videos", options: options).ConfigureAwait(false);
        }
        public async Task<Community> GetChannelCommunityAsync(ulong channelId, RequestOptions options)
        {
            options = RequestOptions.CreateOrClone(options);
            options.AddRequiredScopes("channel_editor");

            try
            {
                return await SendAsync<Community>("GET", () => $"channels/{channelId}/community", options: options).ConfigureAwait(false);
            }
            catch (HttpException ex) when (ex.HttpCode == HttpStatusCode.NotFound) { return null; }
        }
        public async Task SetChannelCommunityAsync(ulong channelId, string communityId, RequestOptions options)
        {
            options = RequestOptions.CreateOrClone(options);
            options.AddRequiredScopes("channel_editor");

            try
            {
                await SendAsync("PUT", () => $"channels/{channelId}/community/{communityId}", options: options).ConfigureAwait(false);
            }
            catch (HttpException ex) when (ex.HttpCode == HttpStatusCode.NoContent) { return; }
        }
        public async Task RemoveChannelCommunityAsync(ulong channelId, RequestOptions options)
        {
            options = RequestOptions.CreateOrClone(options);
            options.AddRequiredScopes("channel_editor");

            try
            {
                await SendAsync("DELETE", () => $"channels/{channelId}/community", options: options).ConfigureAwait(false);
            }
            catch (HttpException ex) when (ex.HttpCode == HttpStatusCode.NoContent) { return; }
        }

        // Chat
        public async Task<CheerData> GetCheersAsync(ulong? channelId, RequestOptions options)
        {
            options = RequestOptions.CreateOrClone(options);
            options.AllowUnauthenticated = true;

            var query = new Dictionary<string, object>
            {
                { "channel_id", channelId }
            };
            
            return await SendAsync<CheerData>("GET", () => $"bits/actions{GetQueryParameters(query)}", options: options).ConfigureAwait(false);
        }
        public async Task<ChatBadges> GetChatBadgesAsync(ulong channelId, RequestOptions options)
        {
            options = RequestOptions.CreateOrClone(options);
            options.AllowUnauthenticated = true;
            return await SendAsync<ChatBadges>("GET", () => $"chat/{channelId}/badges", options: options).ConfigureAwait(false);
        }
        public async Task<EmoteSet> GetEmotesAsync(ulong userId, RequestOptions options)
        {
            options = RequestOptions.CreateOrClone(options);
            options.AddRequiredScopes("user_subscriptions");
            return await SendAsync<EmoteSet>("GET", () => $"users/{userId}/emotes", options: options).ConfigureAwait(false);
        }

        // Communities
        public async Task<Community> GetCommunityAsync(string communityId, bool isName, RequestOptions options)
        {
            options = RequestOptions.CreateOrClone(options);
            options.AllowUnauthenticated = true;

            string query = isName ? $"?name={communityId}" : $"/{communityId}";

            try
            {
                return await SendAsync<Community>("GET", () => $"communities{query}", options: options).ConfigureAwait(false);
            }
            catch (HttpException ex) when (ex.HttpCode == HttpStatusCode.NotFound) { return null; }
        }
        public async Task<CommunityData> GetTopCommunitiesAsync(RequestOptions options) // Supports Paging
        {
            options = RequestOptions.CreateOrClone(options);
            options.AllowUnauthenticated = true;
            return await SendAsync<CommunityData>("GET", () => "communities/top", options: options).ConfigureAwait(false);
        }
        public async Task<CommunityPermissions> GetCommunityPermissionsAsync(string communityId, RequestOptions options)
        {
            options = RequestOptions.CreateOrClone(options);
            return await SendAsync<CommunityPermissions>("GET", () => $"communities/{communityId}/permissions", options: options).ConfigureAwait(false);
        }
        public async Task SendCommunityReportAsync(string communityId, ulong channelId, RequestOptions options)
        {
            options = RequestOptions.CreateOrClone(options);
            options.AllowUnauthenticated = true;

            try
            {
                await SendAsync("GET", () => $"communities/{communityId}/report_channel?channel_id={channelId}", options: options).ConfigureAwait(false);
            }
            catch (HttpException ex) when (ex.HttpCode == HttpStatusCode.NoContent) { }
        }
        public async Task ModifyCommunityAsync(string communityId, ModifyCommunityParams args, RequestOptions options)
        {
            options = RequestOptions.CreateOrClone(options);
            options.AddRequiredScopes("communities_edit");
            
            await SendJsonAsync("PUT", () => $"communitites/{communityId}", args, options: options).ConfigureAwait(false);
        }
        public async Task SetCommunityAvatarAsync(string communityId, string imageBase64, RequestOptions options)
        {
            options = RequestOptions.CreateOrClone(options);
            options.AddRequiredScopes("communities_edit");

            try
            {
                await SendJsonAsync("POST", () => $"communities/{communityId}/images/avatar", new { avatar_image = imageBase64 }, options: options).ConfigureAwait(false);
            }
            catch (HttpException ex) when (ex.HttpCode == HttpStatusCode.NoContent) { }
        }
        public async Task RemoveCommunityAvatarAsync(string communityId, RequestOptions options)
        {
            options = RequestOptions.CreateOrClone(options);
            options.AddRequiredScopes("communities_edit");

            try
            {
                await SendAsync("DELETE", () => $"communities/{communityId}/images/avatar", options: options).ConfigureAwait(false);
            }
            catch (HttpException ex) when (ex.HttpCode == HttpStatusCode.NoContent) { }
        }
        public async Task SetCommunityCoverAsync(string communityId, string imageBase64, RequestOptions options)
        {
            options = RequestOptions.CreateOrClone(options);
            options.AddRequiredScopes("communities_edit");

            try
            {
                await SendJsonAsync("POST", () => $"", new { cover_image = imageBase64 }, options: options).ConfigureAwait(false);
            }
            catch (HttpException ex) when (ex.HttpCode == HttpStatusCode.NoContent) { }
        }
        public async Task RemoveCommunityCoverAsync(string communityId, RequestOptions options)
        {
            options = RequestOptions.CreateOrClone(options);
            options.AddRequiredScopes("communities_edit");

            try
            {
                await SendAsync("DELETE", () => $"communities/{communityId}/images/cover", options: options).ConfigureAwait(false);
            }
            catch (HttpException ex) when (ex.HttpCode == HttpStatusCode.NoContent) { }
        }
        public async Task<CommunityData> GetCommunityModeratorsAsync(string communityId, RequestOptions options)
        {
            options = RequestOptions.CreateOrClone(options);
            options.AddRequiredScopes("communities_edit");
            return await SendAsync<CommunityData>("GET", () => $"communities/{communityId}/moderators", options: options).ConfigureAwait(false);
        }
        public async Task AddCommunityModeratorAsync(string communityId, ulong userId, RequestOptions options)
        {
            options = RequestOptions.CreateOrClone(options);
            options.AddRequiredScopes("communities_edit");

            try
            {
                await SendAsync("Put", $"communities/{communityId}/moderators/{userId}", options: options).ConfigureAwait(false);
            }
            catch (HttpException ex) when (ex.HttpCode == HttpStatusCode.NoContent) { }
        }
        public async Task RemoveCommunityModeratorAsync(string communityId, ulong userId, RequestOptions options)
        {
            options = RequestOptions.CreateOrClone(options);
            options.AddRequiredScopes("communities_edit");

            try
            {
                await SendAsync("DELETE", $"communities/{communityId}/moderators/{userId}", options: options).ConfigureAwait(false);
            }
            catch (HttpException ex) when (ex.HttpCode == HttpStatusCode.NoContent) { }
        }
        public async Task<CommunityData> GetCommunityBansAsync(string communityId, RequestOptions options) // Supports Paging
        {
            options = RequestOptions.CreateOrClone(options);
            options.AddRequiredScopes("communities_moderate");
            return await SendAsync<CommunityData>("GET", () => $"communities/{communityId}/bans", options: options).ConfigureAwait(false);
        }
        public async Task AddCommunityBanAsync(string communityId, ulong userId, RequestOptions options)
        {
            options = RequestOptions.CreateOrClone(options);
            options.AddRequiredScopes("communities_moderate");

            try
            {
                await SendAsync("Put", () => $"communities/{communityId}/bans/{userId}", options: options).ConfigureAwait(false);
            }
            catch (HttpException ex) when (ex.HttpCode == HttpStatusCode.NoContent) { }
        }
        public async Task RemoveCommunityBanAsync(string communityId, ulong userId, RequestOptions options)
        {
            options = RequestOptions.CreateOrClone(options);
            options.AddRequiredScopes("communities_moderate");

            try
            {
                await SendAsync("DELETE", () => $"communities/{communityId}/bans/{userId}", options: options).ConfigureAwait(false);
            }
            catch (HttpException ex) when (ex.HttpCode == HttpStatusCode.NoContent) { }
        }
        public async Task<CommunityData> GetCommunityTimeoutsAsync(string communityId, RequestOptions options) // Supports Paging
        {
            options = RequestOptions.CreateOrClone(options);
            options.AddRequiredScopes("communities_moderate");
            return await SendAsync<CommunityData>("GET", () => $"communities/{communityId}/timeouts", options: options).ConfigureAwait(false);
        }
        public async Task AddCommunityTimeoutAsync(string communityId, ulong userId, RequestOptions options)
        {
            options = RequestOptions.CreateOrClone(options);
            options.AddRequiredScopes("communities_moderate");

            try
            {
                await SendAsync("Put", $"communities/{communityId}/timeouts/{userId}", options: options).ConfigureAwait(false);
            }
            catch (HttpException ex) when (ex.HttpCode == HttpStatusCode.NoContent) { }
        }
        public async Task RemoveCommunityTimeoutAsync(string communityId, ulong userId, RequestOptions options)
        {
            options = RequestOptions.CreateOrClone(options);
            options.AddRequiredScopes("communities_moderate");

            try
            {
                await SendAsync("DELETE", () => $"communities/{communityId}/timeouts/{userId}", options: options).ConfigureAwait(false);
            }
            catch (HttpException ex) when (ex.HttpCode == HttpStatusCode.NoContent) { }
        }

        // Ingests
        public async Task<IngestData> GetIngestsAsync(RequestOptions options)
        {
            options = RequestOptions.CreateOrClone(options);
            options.AllowUnauthenticated = true;
            return await SendAsync<IngestData>("GET", () => "ingests", options: options).ConfigureAwait(false);
        }

        // Broadcasts
        public async Task<BroadcastData> GetBroadcastAsync(ulong channelId, BroadcastType type, RequestOptions options)
        {
            options = RequestOptions.CreateOrClone(options);
            options.AllowUnauthenticated = true;

            string query = type == default(BroadcastType) ? $"?stream_type={type.ToString().ToLower()}" : "";

            try
            {
                return await SendAsync<BroadcastData>("GET", () => $"streams/{channelId}{query}", options: options).ConfigureAwait(false);
            }
            catch (HttpException ex) when (ex.HttpCode == HttpStatusCode.NotFound) { return null; }
        }
        public async Task<BroadcastData> GetBroadcastsAsync(GetBroadcastsParams args, RequestOptions options) // Supports Paging
        {
            options = RequestOptions.CreateOrClone(options);
            options.AllowUnauthenticated = true;

            var query = new Dictionary<string, object>
            {
                { "channel", args.ChannelIds.IsSpecified ? string.Join(",", args.ChannelIds.Value) : null },
                { "game", args.Game.GetValueOrDefault() },
                { "language", args.Language.GetValueOrDefault() },
                { "type", args.Type.GetValueOrDefault() },
                { "limit", args.Limit.GetValueOrDefault() },
                { "offset", args.Offset.GetValueOrDefault() }
            };
            
            return await SendAsync<BroadcastData>("GET", () => $"stream{GetQueryParameters(query)}", options: options).ConfigureAwait(false);
        }
        public async Task<BroadcastData> GetFeaturedBroadcastsAsync(GetFeaturedBroadcastsParams args, RequestOptions options) // Supports Paging
        {
            options = RequestOptions.CreateOrClone(options);
            options.AllowUnauthenticated = true;

            var query = new Dictionary<string, object>
            {
                { "limit", args.Limit.GetValueOrDefault() },
                { "offset", args.Offset.GetValueOrDefault() }
            };
            
            return await SendAsync<BroadcastData>("GET", () => $"streams/featured{GetQueryParameters(query)}", options: options).ConfigureAwait(false);
        }
        public async Task<BroadcastData> GetFollowedBroadcastsAsync(GetFollowedBroadcastsParams args, RequestOptions options) // Supports Paging
        {
            options = RequestOptions.CreateOrClone(options);
            options.AddRequiredScopes("user_read");

            var query = new Dictionary<string, object>
            {
                { "type", args.Type.GetValueOrDefault() },
                { "limit", args.Limit.GetValueOrDefault() },
                { "offset", args.Offset.GetValueOrDefault() }
            };

            return await SendAsync<BroadcastData>("GET", () => $"streams/followed{GetQueryParameters(query)}", options: options).ConfigureAwait(false);
        }
        public async Task<Broadcast> GetBroadcastSummaryAsync(string game, RequestOptions options)
        {
            options = RequestOptions.CreateOrClone(options);
            options.AllowUnauthenticated = true;

            string query = string.IsNullOrWhiteSpace(game) ? "" : $"?game={game}";

            try
            {
                return await SendAsync<Broadcast>("GET", () => $"streams/summary{query}", options: options).ConfigureAwait(false);
            }
            catch (HttpException ex) when (ex.HttpCode == HttpStatusCode.NotFound) { return null; }
        }
        
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
        protected virtual void CheckScopes(IEnumerable<string> requiredScopes)
        {
            if (TokenInfo == null) return;
            if (!requiredScopes.Intersect(TokenInfo.Authorization.Scopes).Any())
                throw new MissingScopeException(requiredScopes);
        }
        protected static double ToMilliseconds(Stopwatch stopwatch) => Math.Round(stopwatch.ElapsedTicks / (double)Stopwatch.Frequency * 1000.0, 2);
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
        private static string GetQueryParameters(Dictionary<string, object> args)
        {
            var builder = new StringBuilder();
            foreach (var arg in args)
            {
                if (builder.Length == 0)
                    builder.Append("?");
                else
                    builder.Append("&");

                if (arg.Value != null)
                    builder.Append($"{arg.Key}={arg.Value}");
            }
            return builder.ToString();
        }
    }
}
