using NTwitch.Rest;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch.Chat
{
    public class ChatChannel : ChatEntity, IChannel
    {
        [ChatProperty(PropertyType.ChannelName)]
        public string Name { get; internal set; }

        public ChatChannel(TwitchChatClient client) : base(client) { }
        
        // Chat
        public Task JoinAsync()
            => throw new NotImplementedException();
        public Task LeaveAsync()
            => throw new NotImplementedException();
        public Task SendMessageAsync(string content)
            => Client.Client.SendAsync($"PRIVMSG #{Name} :{content}");

        // Teams
        public Task<IEnumerable<RestTeam>> GetTeamsAsync()
            => ChannelHelper.GetTeamsAsync(this, Client);

        // Posts
        public Task<IEnumerable<RestPost>> GetPostsAsync()
            => GetPostsAsync(5);
        public Task<IEnumerable<RestPost>> GetPostsAsync(int comments = 5, PageOptions options = null)
            => ChannelHelper.GetPostsAsync(this, Client, comments, options);
        public Task<RestPost> GetPostAsync(ulong id)
            => GetPostAsync(id, 5);
        public Task<RestPost> GetPostAsync(ulong id, int comments = 5)
            => ChannelHelper.GetPostAsync(this, Client, id, comments);

        // Users
        public Task<IEnumerable<RestUserFollow>> GetFollowersAsync()
            => GetFollowersAsync(false);
        public Task<IEnumerable<RestUserFollow>> GetFollowersAsync(bool ascending = false, PageOptions options = null)
            => ChannelHelper.GetFollowersAsync(this, Client, ascending, options);
        public Task FollowAsync()
            => FollowAsync(false);
        public Task FollowAsync(bool notify = false)
            => ChannelHelper.FollowAsync(this, Client, notify);
        public Task UnfollowAsync()
            => ChannelHelper.UnfollowAsync(this, Client);

        // Videos
        public Task<RestStream> GetStreamAsync()
            => ClientHelper.GetStreamAsync(Client, Id, StreamType.All);
        public Task<IEnumerable<RestVideo>> GetVideosAsync()
            => GetVideosAsync(null);
        public Task<IEnumerable<RestVideo>> GetVideosAsync(string language = null, SortMode sort = SortMode.CreatedAt, BroadcastType type = BroadcastType.Highlight, PageOptions options = null)
            => ChannelHelper.GetVideosAsync(this, Client, language, sort, type, options);
        public Task<IEnumerable<RestClip>> GetTopClipsAsync(string game)
            => GetTopClipsAsync(game);
        public Task<IEnumerable<RestClip>> GetTopClipsAsync(string game, VideoPeriod period = VideoPeriod.Week, bool istrending = false, PageOptions options = null)
            => ChannelHelper.GetTopClipsAsync(this, Client, game, period, istrending, options);
        public Task<RestClip> GetClipAsync(string id)
            => ChannelHelper.GetClipAsync(this, Client, id);

        // IChannel
        Task IChannel.FollowAsync(bool notify)
            => FollowAsync(notify);
        Task IChannel.UnfollowAsync()
            => UnfollowAsync();
        Task IChannel.GetPostsAsync()
            => GetPostsAsync();
        Task IChannel.GetPostAsync()
            => GetPostsAsync();
        Task IChannel.GetFollowersAsync()
            => GetFollowersAsync();
        Task IChannel.GetTeamsAsync()
            => GetTeamsAsync();
        Task IChannel.GetVideosAsync()
            => GetVideosAsync();
        Task IChannel.GetStreamAsync()
            => GetStreamAsync();
        Task IChannel.GetTopClipsAsync(string game, VideoPeriod period, bool istrending, PageOptions options)
            => GetTopClipsAsync(game, period, istrending, options);
        Task IChannel.GetClipAsync(string clipId)
            => GetClipAsync(clipId);
    }
}
