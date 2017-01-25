using NTwitch.Rest;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch.Chat
{
    public class ChatUser : UserBase
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
    }
}
