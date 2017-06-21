using NTwitch.Chat.Queue;
using System.Linq;

namespace NTwitch.Chat.API
{
    internal class PartEvent
    {
        // Parameters
        public string ChannelName { get; set; }

        // Prefix
        public string UserName { get; set; }

        internal static PartEvent Create(ChatResponse msg)
        {
            var entity = new PartEvent();
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
