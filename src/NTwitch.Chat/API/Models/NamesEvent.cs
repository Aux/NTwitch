using NTwitch.Chat.Queue;
using System.Linq;

namespace NTwitch.Chat.API
{
    internal class NamesEvent
    {
        // Parameters
        public string UserName { get; set; }
        public string ChannelName { get; set; }
        public string[] Names { get; set; }

        internal static NamesEvent Create(ChatResponse msg)
        {
            var entity = new NamesEvent();
            entity.Update(msg);
            return entity;
        }

        internal void Update(ChatResponse msg)
        {
            UserName = msg.Parameters.ElementAt(0);
            ChannelName = msg.Parameters.ElementAt(2).Substring(1).Trim();
            Names = msg.Parameters.Last().Split(' ');
        }
    }
}
