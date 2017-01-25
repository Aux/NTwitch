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
        [ChatProperty("mod")]
        public bool IsModerator { get; internal set; }
        [ChatProperty("subscriber")]
        public bool IsSubscriber { get; internal set; }
        [ChatProperty("turbo")]
        public bool IsTurbo { get; internal set; }
        
        public ChatUser(TwitchChatClient client) : base(client) { }
        
        public Task BanAsync(ChatChannel channel, int? duration = null)
            => BanAsync(channel.Name, duration);
        public Task BanAsync(string channelName, int? duration = null)
            => throw new NotImplementedException();

        // IUser

        Task<IBlockedUser> IUser.BlockAsync()
            => throw new NotImplementedException();
        Task<IBlockedUser> IUser.UnblockAsync()
            => throw new NotImplementedException();
        Task<bool> IUser.IsFollowingAsync(ulong channelId)
            => throw new NotImplementedException();
        Task<IChannelFollow> IUser.GetFollowsAsync()
            => throw new NotImplementedException();
    }
}
