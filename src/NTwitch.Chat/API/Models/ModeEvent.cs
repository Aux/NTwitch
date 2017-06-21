using NTwitch.Chat.Queue;
using System.Linq;

namespace NTwitch.Chat.API
{
    internal class ModeEvent
    {
        // Parameters
        public string ChannelName { get; set; }
        public string Type { get; set; }
        public string UserName { get; set; }

        internal static ModeEvent Create(ChatResponse msg)
        {
            var entity = new ModeEvent();
            entity.Update(msg);
            return entity;
        }

        internal void Update(ChatResponse msg)
        {
            ChannelName = msg.Parameters.First().Substring(1);
            Type = msg.Parameters.Skip(1).First();
            UserName = msg.Parameters.Last();
        }
    }
}
