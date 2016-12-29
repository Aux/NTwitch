using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch.WebSocket
{
    public partial class TwitchSocketClient : ITwitchClient
    {
        private SocketApiClient SocketClient { get; }
        public string SocketUrl { get; }

        public TwitchSocketClient() : this(new TwitchSocketConfig()) { }
        public TwitchSocketClient(TwitchSocketConfig config)
        {
            SocketUrl = config.SocketUrl;
        }

        public Task<IChannel> GetChannelAsync(ulong id)
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

        public Task<IEnumerable<IChannel>> FindChannelsAsync(string query, TwitchPageOptions options = null)
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

        public Task<IEnumerable<ITeamSummary>> GetTeamsAsync(TwitchPageOptions options = null)
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
        public ConnectionState ConnectionState { get; } = ConnectionState.Disconnected;

        public Task LoginAsync(string clientid, string token = null)
        {
            throw new NotImplementedException();
        }

        public Task ConnectAsync()
        {
            throw new NotImplementedException();
        }

        public Task DisconnectAsync()
        {
            throw new NotImplementedException();
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
        async Task<IEnumerable<ITeamSummary>> ITwitchClient.GetTeamsAsync(TwitchPageOptions options)
            => await GetTeamsAsync(options);
        async Task<IEnumerable<ITeam>> ITwitchClient.GetTeamAsync(string name)
            => await GetTeamAsync(name);
        async Task<IUser> ITwitchClient.GetUserAsync(ulong id)
            => await GetUserAsync(id);
    }
}
