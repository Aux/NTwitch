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

        public Task<RestChannel> GetChannelAsync(ulong id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ITopGame>> GetTopGamesAsync(TwitchPageOptions options = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IIngest>> GetIngestsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RestChannel>> FindChannelsAsync(string query, TwitchPageOptions options = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IGame>> FindGamesAsync(string query, bool islive = true)
        {
            throw new NotImplementedException();
        }

        public Task<IStream> GetStreamAsync(ulong id, StreamType type = StreamType.All)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IStream>> FindStreamsAsync(string query, bool hls = true, TwitchPageOptions options = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IStream>> GetStreamsAsync(string game = null, ulong[] channelids = null, string language = null, StreamType type = StreamType.All, TwitchPageOptions options = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IFeaturedStream>> GetFeaturedStreamsAsync(TwitchPageOptions options = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IStreamSummary>> GetStreamSummaryAsync(string game)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ITeamInfo>> GetTeamsAsync(TwitchPageOptions options = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ITeam>> GetTeamAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IUser> GetUserAsync(ulong id)
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
        async Task<IEnumerable<ITeamInfo>> ITwitchClient.GetTeamsAsync(TwitchPageOptions options)
            => await GetTeamsAsync(options);
        async Task<IEnumerable<ITeam>> ITwitchClient.GetTeamAsync(string name)
            => await GetTeamAsync(name);
        async Task<IUser> ITwitchClient.GetUserAsync(ulong id)
            => await GetUserAsync(id);
    }
}
