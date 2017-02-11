using NTwitch.Rest;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch.Chat
{
    public class ChatUser : ChatEntity<ulong>, IUser
    {
        [ChatProperty("badges")]
        public string Badges { get; internal set; }
        [ChatProperty("color")]
        public string Color { get; internal set; }
        [ChatProperty("display-name")]
        public string DisplayName { get; internal set; }
        [ChatProperty("user-type")]
        public string Type { get; internal set; }
        [ChatProperty("mod")]
        public bool IsModerator { get; internal set; }
        [ChatProperty("subscriber")]
        public bool IsSubscriber { get; internal set; }
        [ChatProperty("turbo")]
        public bool IsTurbo { get; internal set; }

        public ChatUser(TwitchChatClient client) : base(client) { }
        
        // Users
        public Task BanAsync(ChatChannel channel, int? duration = null)
            => BanAsync(channel.Name, duration);
        public Task BanAsync(string channelName, int? duration = null)
            => throw new NotImplementedException();

        // Users
        public Task<RestBlockedUser> BlockAsync()
            => UserHelper.BlockAsync(this, Client, Id);
        public Task<RestChannelFollow> GetFollowAsync(uint channelId)
            => UserHelper.GetFollowAsync(this, Client, channelId);
        public Task<IEnumerable<RestChannelFollow>> GetFollowsAsync()
            => GetFollowsAsync(SortMode.CreatedAt);
        public Task<IEnumerable<RestChannelFollow>> GetFollowsAsync(SortMode mode = SortMode.CreatedAt, bool ascending = false, PageOptions options = null)
            => UserHelper.GetFollowsAsync(this, Client, mode, ascending, options);
        public Task UnblockAsync()
            => UserHelper.UnblockAsync(this, Client, Id);

        // IUser
        Task<IBlockedUser> IUser.BlockAsync() => null;
        Task IUser.UnblockAsync() => null;
        Task<IChannelFollow> IUser.GetFollowAsync(uint channelId) => null;
        Task<IEnumerable<IChannelFollow>> IUser.GetFollowsAsync() => null;
    }
}
