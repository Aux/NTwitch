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

        public Task<IChannel> GetChannelAsync(ulong id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ITopGame>> GetTopGames(TwitchPageOptions options = null)
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
    }
}
