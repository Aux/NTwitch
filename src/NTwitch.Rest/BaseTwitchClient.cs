using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("NTwitch.Pubsub")]
[assembly: InternalsVisibleTo("NTwitch.Chat")]
namespace NTwitch.Rest
{
    public abstract class BaseTwitchClient : ITwitchClient
    {
        internal LogManager _log;

        private readonly AsyncEvent<Func<LogMessage, Task>> _logEvent = new AsyncEvent<Func<LogMessage, Task>>();
        public event Func<LogMessage, Task> Log
        {
            add { _logEvent.Add(value); }
            remove { _logEvent.Remove(value); }
        }

        internal BaseTwitchClient(TwitchRestConfig config)
        {
            _log = new LogManager(config.LogLevel);
            _log.LogReceived += OnLogReceived;
        }

        private Task OnLogReceived(LogMessage msg)
            => _logEvent.InvokeAsync(msg);

        // ITwitchClient
        Task<ISelfUser> ITwitchClient.GetCurrentUserAsync()
            => Task.FromResult<ISelfUser>(null);
        Task<ISelfChannel> ITwitchClient.GetCurrentChannelAsync()
            => Task.FromResult<ISelfChannel>(null);
        Task<IChannel> ITwitchClient.GetChannelAsync(ulong id)
            => Task.FromResult<IChannel>(null);
        Task<IEnumerable<ITopGame>> ITwitchClient.GetTopGamesAsync(TwitchPageOptions options)
            => Task.FromResult<IEnumerable<ITopGame>>(null);
        Task<IEnumerable<IIngest>> ITwitchClient.GetIngestsAsync()
            => Task.FromResult<IEnumerable<IIngest>>(null);
        Task<IEnumerable<IChannel>> ITwitchClient.FindChannelsAsync(string query, TwitchPageOptions options)
            => Task.FromResult<IEnumerable<IChannel>>(null);
        Task<IEnumerable<IGame>> ITwitchClient.FindGamesAsync(string query, bool islive)
            => Task.FromResult<IEnumerable<IGame>>(null);
        Task<IStream> ITwitchClient.GetStreamAsync(ulong id, StreamType type)
            => Task.FromResult<IStream>(null);
        Task<IEnumerable<IStream>> ITwitchClient.FindStreamsAsync(string query, bool hls, TwitchPageOptions options)
            => Task.FromResult<IEnumerable<IStream>>(null);
        Task<IEnumerable<IStream>> ITwitchClient.GetStreamsAsync(string game, ulong[] channelids, string language, StreamType type, TwitchPageOptions options)
            => Task.FromResult<IEnumerable<IStream>>(null);
        Task<IEnumerable<IFeaturedStream>> ITwitchClient.GetFeaturedStreamsAsync(TwitchPageOptions options)
            => Task.FromResult<IEnumerable<IFeaturedStream>>(null);
        Task<IStreamSummary> ITwitchClient.GetStreamSummaryAsync(string game)
            => Task.FromResult<IStreamSummary>(null);
        Task<IEnumerable<ITeamSummary>> ITwitchClient.GetTeamsAsync(TwitchPageOptions options)
            => Task.FromResult<IEnumerable<ITeamSummary>>(null);
        Task<ITeam> ITwitchClient.GetTeamAsync(string name)
            => Task.FromResult<ITeam>(null);
        Task<IUser> ITwitchClient.GetUserAsync(ulong id)
            => Task.FromResult<IUser>(null);
        Task<IEnumerable<IUser>> ITwitchClient.FindUsersAsync(string name)
            => Task.FromResult<IEnumerable<IUser>>(null);
        Task<IEnumerable<IVideo>> ITwitchClient.GetTopVideosAsync(string game, VideoPeriod period, BroadcastType type, TwitchPageOptions options)
            => Task.FromResult<IEnumerable<IVideo>>(null);
    }
}
