using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public partial class TwitchRestClient : ITwitchClient
    {
        private RestApiClient _rest;
        private LogManager _log;
        public string BaseUrl { get; }

        public TwitchRestClient() : this(new TwitchRestConfig()) { }
        public TwitchRestClient(TwitchRestConfig config)
        {
            BaseUrl = config.BaseUrl;

            _log = new LogManager(config.LogLevel);
            _log.LogReceived += OnLogReceived;
        }

        private async Task OnLogReceived(LogMessage msg)
            => await _logEvent.InvokeAsync(msg);

        public Task<RestSelfUser> GetCurrentUserAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RestSelfChannel> GetCurrentChannelAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RestChannel> GetChannelAsync(ulong id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RestTopGame>> GetTopGamesAsync(TwitchPageOptions options = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RestIngest>> GetIngestsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RestChannel>> FindChannelsAsync(string query, TwitchPageOptions options = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RestGame>> FindGamesAsync(string query, bool islive = true)
        {
            throw new NotImplementedException();
        }

        public Task<RestStream> GetStreamAsync(ulong id, StreamType type = StreamType.All)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RestStream>> FindStreamsAsync(string query, bool hls = true, TwitchPageOptions options = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RestStream>> GetStreamsAsync(string game = null, ulong[] channelids = null, string language = null, StreamType type = StreamType.All, TwitchPageOptions options = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RestFeaturedStream>> GetFeaturedStreamsAsync(TwitchPageOptions options = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RestStreamSummary>> GetStreamSummaryAsync(string game)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RestTeamSummary>> GetTeamsAsync(TwitchPageOptions options = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RestTeam>> GetTeamAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<RestUser> GetUserAsync(ulong id)
        {
            throw new NotImplementedException();
        }

        public Task<RestUser> GetUserAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RestVideo>> GetTopVideosAsync(string game = null, VideoPeriod period = VideoPeriod.Week, BroadcastType type = BroadcastType.Highlight, TwitchPageOptions options = null)
        {
            throw new NotImplementedException();
        }

        // ITwitchClient
        public ConnectionState ConnectionState { get; private set; } = ConnectionState.Disconnected;

        /// <summary> Authenticate this client with the twitch oauth servers. </summary>
        public async Task LoginAsync(string clientid, string token = null)
        {
            if (string.IsNullOrWhiteSpace(clientid))
                throw new ArgumentNullException("clientid");

            _rest = new RestApiClient(_log, BaseUrl, clientid, token);
            await _log.DebugAsync("TwitchRestClient", "RestApiClient created successfully");

            if (!string.IsNullOrWhiteSpace(token))
            {
                await _log.DebugAsync("TwitchRestClient", "Validating token");
                await _rest.LoginAsync(token);
            }
        }

        async Task<ISelfUser> ITwitchClient.GetCurrentUserAsync()
            => await GetCurrentUserAsync();
        async Task<ISelfChannel> ITwitchClient.GetCurrentChannelAsync()
            => await GetCurrentChannelAsync();
        async Task<IChannel> ITwitchClient.GetChannelAsync(ulong id)
            => await GetChannelAsync(id);
        async Task<IEnumerable<ITopGame>> ITwitchClient.GetTopGamesAsync(TwitchPageOptions options)
            => await GetTopGamesAsync(options);
        async Task<IEnumerable<IIngest>> ITwitchClient.GetIngestsAsync()
            => await GetIngestsAsync();
        async Task<IEnumerable<IChannel>> ITwitchClient.FindChannelsAsync(string query, TwitchPageOptions options)
            => await FindChannelsAsync(query, options);
        async Task<IEnumerable<IGame>> ITwitchClient.FindGamesAsync(string query, bool islive)
            => await FindGamesAsync(query, islive);
        async Task<IStream> ITwitchClient.GetStreamAsync(ulong id, StreamType type)
            => await GetStreamAsync(id, type);
        async Task<IEnumerable<IStream>> ITwitchClient.FindStreamsAsync(string query, bool hls, TwitchPageOptions options)
            => await FindStreamsAsync(query, hls, options);
        async Task<IEnumerable<IStream>> ITwitchClient.GetStreamsAsync(string game, ulong[] channelids, string language, StreamType type, TwitchPageOptions options)
            => await GetStreamsAsync(game, channelids, language, type, options);
        async Task<IEnumerable<IFeaturedStream>> ITwitchClient.GetFeaturedStreamsAsync(TwitchPageOptions options)
            => await GetFeaturedStreamsAsync(options);
        async Task<IEnumerable<IStreamSummary>> ITwitchClient.GetStreamSummaryAsync(string game)
            => await GetStreamSummaryAsync(game);
        async Task<IEnumerable<ITeamSummary>> ITwitchClient.GetTeamsAsync(TwitchPageOptions options)
            => await GetTeamsAsync(options);
        async Task<IEnumerable<ITeam>> ITwitchClient.GetTeamAsync(string name)
            => await GetTeamAsync(name);
        async Task<IUser> ITwitchClient.GetUserAsync(ulong id)
            => await GetUserAsync(id);
        async Task<IUser> ITwitchClient.GetUserAsync(string name)
            => await GetUserAsync(name);
        async Task<IEnumerable<IVideo>> ITwitchClient.GetTopVideosAsync(string game, VideoPeriod period, BroadcastType type, TwitchPageOptions options)
            => await GetTopVideosAsync(game, period, type, options);
    }
}
