using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch.Chat
{
    public class ChatUser : ChatEntity
    {
        public ulong Id { get; private set; }
        public IEnumerable<string> Badges { get; private set; }
        public string Color { get; private set; }
        public string DisplayName { get; private set; }
        public bool IsModerator { get; private set; }
        public bool IsSubscriber { get; private set; }
        public bool IsTurbo { get; private set; }
        
        public ChatUser(TwitchChatClient client) : base(client) { }

        public static ChatUser Create(Dictionary<string, string> data)
        {
            return new ChatUser(null)
            {
                Id = ulong.Parse(data["user-id"]),
                Badges = data["badges"].Split(','),
                Color = data["color"],
                DisplayName = data["display-name"],
                IsModerator = data["mod"] == "1",
                IsSubscriber = data["subscriber"] == "1",
                IsTurbo = data["turbo"] == "1"
            };
        }

        public Task BanAsync(string channel, int? duration = null)
        {
            throw new NotImplementedException();
        }

        public Task BanAsync(ChatChannel channel, int? duration = null)
        {
            throw new NotImplementedException();
        }
    }
}
