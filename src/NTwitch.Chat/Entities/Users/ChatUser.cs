using NTwitch.Rest;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch.Chat
{
    public class ChatUser : ChatEntity, IUser
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
        
        // Rest
        public Task<RestBlockedUser> BlockAsync()
            => UserHelper.BlockAsync(this, Client);
        public Task<bool> IsFollowingAsync(ulong channelId)
            => UserHelper.IsFollowingAsync(this, Client, channelId);
        public Task<IEnumerable<RestChannelFollow>> GetFollowsAsync()
            => GetFollowsAsync();
        public Task<IEnumerable<RestChannelFollow>> GetFollowsAsync(SortMode mode = SortMode.CreatedAt, SortDirection direction = SortDirection.Descending, PageOptions options = null)
            => UserHelper.GetFollowsAsync(this, Client, mode, direction, options);
        public Task<RestBlockedUser> UnblockAsync()
            => UserHelper.UnblockAsync(this, Client);

        // IUser
        Task<IBlockedUser> IUser.BlockAsync()
            => throw new NotImplementedException();
        Task<IBlockedUser> IUser.UnblockAsync()
            => throw new NotImplementedException();
        Task<bool> IUser.IsFollowingAsync(ulong channelId)
            => throw new NotImplementedException();
        Task<IEnumerable<IChannelFollow>> IUser.GetFollowsAsync()
            => throw new NotImplementedException();
    }
}
