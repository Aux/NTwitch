using NTwitch.Chat.Queue;
using System.Linq;

namespace NTwitch.Chat.API
{
    internal class RoomStateEvent
    {
        // Tags
        public ulong ChannelId { get; set; }
        public string BroadcasterLang { get; set; }
        public int FollowersOnlyMode { get; set; }
        public bool IsEmoteOnly { get; set; }
        public bool IsR9k { get; set; }
        public bool IsSlow { get; set; }
        public bool IsSubsOnly { get; set; }

        // Parameters
        public string ChannelName { get; set; } // First

        internal static RoomStateEvent Create(ChatResponse msg)
        {
            var entity = new RoomStateEvent();
            entity.Update(msg);
            return entity;
        }

        internal void Update(ChatResponse msg)
        {
            ChannelId = ulong.Parse(msg.Tags["room-id"]);
            BroadcasterLang = msg.Tags["broadcaster-lang"];
            FollowersOnlyMode = int.Parse(msg.Tags["followers-only"]);
            IsEmoteOnly = msg.Tags["emote-only"] == "1";
            IsR9k = msg.Tags["r9k"] == "1";
            IsSlow = msg.Tags["slow"] == "1";
            IsSubsOnly = msg.Tags["subs-only"] == "1";
            
            ChannelName = msg.Parameters.First().Substring(1).Trim();
        }
    }
}
