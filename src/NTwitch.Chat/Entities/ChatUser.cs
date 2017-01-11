using NTwitch.Rest;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch.Chat
{
    public class ChatUser : BaseUser
    {
        public IEnumerable<string> Badges { get; private set; }
        public string Color { get; private set; }
        public string DisplayName { get; private set; }
        public bool IsModerator { get; private set; }
        public bool IsSubscriber { get; private set; }
        public bool IsTurbo { get; private set; }

        public ChatUser(TwitchChatClient client) : base(client) { }

        public static ChatUser Create(BaseRestClient client, string tcf)
        {
            var user = new ChatUser(client as TwitchChatClient);
            TcfConvert.PopulateObject(tcf, user);
            return user;
        }

        public Task BanAsync(string channel, int? duration = null)
            => throw new NotImplementedException();

        public Task BanAsync(ChatChannel channel, int? duration = null)
            => throw new NotImplementedException();
    }
}
