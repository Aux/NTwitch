using NTwitch.Rest;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch.Chat
{
    public class ChatUser : UserBase
    {
        [ChatProperty("badges")]
        public IEnumerable<string> Badges { get; private set; }
        [ChatProperty("color")]
        public string Color { get; private set; }
        [ChatProperty("display-name")]
        public string DisplayName { get; private set; }
        [ChatProperty("mod")]
        public bool IsModerator { get; private set; }
        [ChatProperty("subscriber")]
        public bool IsSubscriber { get; private set; }
        [ChatProperty("turbo")]
        public bool IsTurbo { get; private set; }

        public ChatUser(TwitchChatClient client) : base(client) { }

        public static ChatUser Create(BaseRestClient client, string msg)
        {
            var user = new ChatUser(client as TwitchChatClient);
            ChatParser.PopulateObject(msg, user);
            return user;
        }

        public Task BanAsync(string channel, int? duration = null)
            => throw new NotImplementedException();

        public Task BanAsync(ChatChannel channel, int? duration = null)
            => throw new NotImplementedException();
    }
}
