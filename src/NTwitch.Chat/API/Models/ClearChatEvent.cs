using NTwitch.Chat.Queue;
using System.Linq;

namespace NTwitch.Chat.API
{
    internal class ClearChatEvent
    {
        // Tags
        public string Reason { get; set; }
        public long? Duration { get; set; }
        public ulong ChannelId { get; set; }
        public ulong UserId { get; set; }

        // Parameters
        public string ChannelName { get; set; }  // First
        public string UserName { get; set; }     // Last

        internal static ClearChatEvent Create(ChatResponse msg)
        {
            var entity = new ClearChatEvent();
            entity.Update(msg);
            return entity;
        }

        internal void Update(ChatResponse msg)
        {
            if (long.TryParse(msg.Tags["ban-duration"], out long duration))
                Duration = duration;
            else
                Duration = null;
            
            Reason = msg.Tags["ban-reason"];
            ChannelId = ulong.Parse(msg.Tags["room-id"]);
            UserId = ulong.Parse(msg.Tags["target-user-id"]);

            ChannelName = msg.Parameters.First();
            UserName = msg.Parameters.Last();
        }
    }
}
