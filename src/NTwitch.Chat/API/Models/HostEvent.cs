using NTwitch.Chat.Queue;
using System.Linq;

namespace NTwitch.Chat.API
{
    internal class HostEvent
    {
        // Parameters
        public string HostName { get; set; }
        public string ChannelName { get; set; }
        public int Viewers { get; set; }
        
        internal static HostEvent Create(ChatResponse msg)
        {
            var entity = new HostEvent();
            entity.Update(msg);
            return entity;
        }

        internal void Update(ChatResponse msg)
        {
            HostName = msg.Parameters.First().Substring(1).Trim();

            var split = msg.Parameters.Last().Split(' ');
            Viewers = int.Parse(split.Last());

            if (split.First() != "-")
                ChannelName = split.First();
        }
    }
}
