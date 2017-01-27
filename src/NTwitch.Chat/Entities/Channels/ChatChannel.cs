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

        // Rest
        // Teams
        public Task<IEnumerable<RestTeam>> GetTeamsAsync()
            => ChannelHelper.GetTeamsAsync(this, Client);

        // Posts
        public Task<IEnumerable<RestPost>> GetPostsAsync()
            => GetPostsAsync();
        public Task<IEnumerable<RestPost>> GetPostsAsync(int comments = 5, PageOptions options = null)
            => ChannelHelper.GetPostsAsync(this, Client, comments, options);
        public Task<RestPost> GetPostAsync(ulong id)
            => GetPostAsync(id);
        public Task<RestPost> GetPostAsync(ulong id, int comments = 5)
            => ChannelHelper.GetPostAsync(this, Client, id, comments);
        
        // Users
        public Task<IEnumerable<RestUserFollow>> GetFollowersAsync()
            => GetFollowersAsync();
        public Task<IEnumerable<RestUserFollow>> GetFollowersAsync(SortDirection direction = SortDirection.Descending, PageOptions options = null)
            => ChannelHelper.GetFollowersAsync(this, Client, direction, options);
        public Task FollowAsync()
            => FollowAsync();
        public Task FollowAsync(bool notify = false)
            => ChannelHelper.FollowAsync(this, Client, notify);
        public Task UnfollowAsync()
            => ChannelHelper.UnfollowAsync(this, Client);

        // Videos
        public Task<RestStream> GetStreamAsync()
            => ClientHelper.GetStreamAsync(Client, Id, StreamType.All);
        public Task<IEnumerable<RestVideo>> GetVideosAsync()
            => GetVideosAsync();
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
            => throw new NotImplementedException();
        Task IChannel.GetClipAsync(string clipId)
            => throw new NotImplementedException();
        Task IChannel.GetFollowersAsync()
            => throw new NotImplementedException();
        Task IChannel.GetPostAsync()
            => throw new NotImplementedException();
        Task IChannel.GetPostsAsync()
            => throw new NotImplementedException();
        Task IChannel.GetStreamAsync()
            => throw new NotImplementedException();
        Task IChannel.GetTeamsAsync()
            => throw new NotImplementedException();
        Task IChannel.GetTopClipsAsync(string game, VideoPeriod period, bool istrending, PageOptions options)
            => throw new NotImplementedException();
        Task IChannel.GetVideosAsync()
            => throw new NotImplementedException();
        Task IChannel.UnfollowAsync()
            => throw new NotImplementedException();
    }
}
