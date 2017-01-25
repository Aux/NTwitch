using System;
using System.Threading.Tasks;

namespace NTwitch.Chat
{
    public class ChatChannel : ChatEntity, IChannel
    {
        [ChatProperty(PropertyType.ChannelName)]
        public string Name { get; internal set; }

        public ChatChannel(TwitchChatClient client) : base(client) { }
        
        public Task JoinAsync()
            => throw new NotImplementedException();
        public Task LeaveAsync()
            => throw new NotImplementedException();

        // IChannel

        Task IChannel.FollowAsync(bool notify)
            => throw new NotImplementedException();
        Task IChannel.GetBadgesAsync()
            => throw new NotImplementedException();
        Task IChannel.GetClipAsync(string clipId)
            => throw new NotImplementedException();
        Task IChannel.GetEmotesASync()
            => throw new NotImplementedException();
        Task IChannel.GetEmoteSetAsync(ulong setId)
            => throw new NotImplementedException();
        Task IChannel.GetEmoteSetsAsync()
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
        Task IChannel.GetTopClipsAsync()
            => throw new NotImplementedException();
        Task IChannel.GetVideosAsync()
            => throw new NotImplementedException();
        Task IChannel.UnfollowAsync()
            => throw new NotImplementedException();
    }
}
