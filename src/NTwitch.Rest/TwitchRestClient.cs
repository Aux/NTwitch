using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    /// <summary>  </summary>
    public partial class TwitchRestClient : BaseTwitchClient, ITwitchClient
    {
        public TwitchRestClient() : this(new TwitchRestConfig()) { }
        public TwitchRestClient(TwitchRestConfig config) : base(config)
        {
            _log.LogReceived += OnLogReceived;
        }

        private async Task OnLogReceived(LogMessage msg)
            => await _logEvent.InvokeAsync(msg);

        public async Task<RestSelfUser> GetCurrentUserAsync()
            => await ClientHelper.GetCurrentUserAsync(this);

        public async Task<RestSelfChannel> GetCurrentChannelAsync()
            => await ClientHelper.GetCurrentChannelAsync(this);
        
        public async Task<RestChannel> GetChannelAsync(ulong id)
            => await ClientHelper.GetChannelAsync(this, id);
        
        public async Task<IEnumerable<RestTopGame>> GetTopGamesAsync(TwitchPageOptions options = null)
            => await ClientHelper.GetTopGamesAsync(this, options);
        
        public async Task<IEnumerable<RestIngest>> GetIngestsAsync()
            => await ClientHelper.GetIngestsAsync(this);
        
        public async Task<IEnumerable<RestChannel>> FindChannelsAsync(string query, TwitchPageOptions options = null)
            => await ClientHelper.FindChannelsAsync(this, query, options);
        
        public async Task<IEnumerable<RestGame>> FindGamesAsync(string query, bool islive = true)
            => await ClientHelper.FindGamesAsync(this, query, islive);
        
        public async Task<RestStream> GetStreamAsync(ulong id, StreamType type = StreamType.All)
            => await ClientHelper.GetStreamAsync(this, id, type);
        
        public async Task<IEnumerable<RestStream>> FindStreamsAsync(string query, bool hls = true, TwitchPageOptions options = null)
            => await ClientHelper.FindStreamsAsync(this, query, hls, options);
        
        public async Task<IEnumerable<RestStream>> GetStreamsAsync(string game = null, ulong[] channelids = null, string language = null, StreamType type = StreamType.All, TwitchPageOptions options = null)
            => await ClientHelper.GetStreamsAsync(this, game, channelids, language, type, options);
        
        public async Task<IEnumerable<RestFeaturedStream>> GetFeaturedStreamsAsync(TwitchPageOptions options = null)
            => await ClientHelper.GetFeaturedStreamsAsync(this, options);
        
        public async Task<RestStreamSummary> GetStreamSummaryAsync(string game)
            => await ClientHelper.GetStreamSummaryAsync(this, game);
        
        public async Task<IEnumerable<RestTeamSummary>> GetTeamsAsync(TwitchPageOptions options = null)
            => await ClientHelper.GetTeamsAsync(this, options);
        
        public async Task<RestTeam> GetTeamAsync(string name)
            => await ClientHelper.GetTeamAsync(this, name);
        
        public async Task<RestUser> GetUserAsync(ulong id)
            => await ClientHelper.GetUserAsync(this, id);
        
        public async Task<IEnumerable<RestUser>> FindUsersAsync(string name)
            => await ClientHelper.FindUsersAsync(this, name);
        
        public async Task<IEnumerable<RestVideo>> GetTopVideosAsync(string game = null, VideoPeriod period = VideoPeriod.Week, BroadcastType type = BroadcastType.Highlight, TwitchPageOptions options = null)
            => await ClientHelper.GetTopVideosAsync(this, game, period, type, options);
    }
}
