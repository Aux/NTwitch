using NTwitch.Chat.Queue;
using System.Linq;

namespace NTwitch.Chat.API
{
    internal class JoinEvent
    {
        // Parameters
        public string ChannelName { get; set; }

        // Prefix
        public string UserName { get; set; }

        internal static JoinEvent Create(ChatResponse msg)
        {
            var entity = new JoinEvent();
            entity.Update(msg);
            return entity;
        }

        internal void Update(ChatResponse msg)
        {
            int nameEndIndex = msg.Prefix.IndexOf('!');
            UserName = msg.Prefix.Substring(0, nameEndIndex);

            ChannelName = msg.Parameters.First();
        }
    }
}
