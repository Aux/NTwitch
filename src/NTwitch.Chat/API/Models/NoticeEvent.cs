using NTwitch.Chat.Queue;
using System.Linq;

namespace NTwitch.Chat
{
    internal class NoticeEvent
    {
        // Tags
        public string Id { get; set; }

        // Parameters
        public string ChannelName { get; set; } // First
        public string Content { get; set; }     // Last
        
        internal static NoticeEvent Create(ChatResponse msg)
        {
            var entity = new NoticeEvent();
            entity.Update(msg);
            return entity;
        }

        internal void Update(ChatResponse msg)
        {
            Id = msg.Tags["msg-id"];

            ChannelName = msg.Parameters.First().Substring(1);
            Content = msg.Parameters.Last();
        }
    }
}
