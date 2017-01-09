using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch.Chat
{
    public class ChatMessage : ChatEntity
    {
        public string Id { get; private set; }
        public DateTime UtcTimestamp { get; private set; } = DateTime.UtcNow;
        public IEnumerable<string> Emotes { get; private set; }
        public ChatChannel Channel { get; private set; }
        public ChatUser User { get; private set; }
        public string Content { get; private set; }
        
        public ChatMessage(TwitchChatClient client) : base(client) { }

        public static ChatMessage Create(Dictionary<string, string> data)
        {
            var msg = new ChatMessage(null)
            {
                Id = data["id"],
                User = ChatUser.Create(data),
                Channel = ChatChannel.Create(data)
            };

            string usertype = data["user-type"];
            int index = usertype.IndexOf("PRIVMSG #" + msg.Channel.Name + " :");
            string content = usertype.Substring(index);
            msg.Content = content;

            return msg;
        }
    }
}
