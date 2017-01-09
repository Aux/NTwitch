using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch.Chat
{
    public class ChatChannel : ChatEntity
    {
        public ulong Id { get; private set; }
        public string Name { get; private set; }
        
        public ChatChannel(TwitchChatClient client) : base(client) { }

        public static ChatChannel Create(Dictionary<string, string> data)
        {
            string usertype = data["user-type"];
            int hashindex = usertype.IndexOf("#");
            string partial = usertype.Substring(hashindex);
            int endindex = partial.IndexOf(' ');
            string name = partial.Substring(0, endindex);

            return new ChatChannel(null)
            {
                Id = ulong.Parse(data["room-id"]),
                Name = name
            };
        }

        public Task JoinAsync()
        {
            throw new NotImplementedException();
        }

        public Task LeaveAsync()
        {
            throw new NotImplementedException();
        }

        public Task SendMessageAsync(string content)
        {
            throw new NotImplementedException();
        }
    }
}
