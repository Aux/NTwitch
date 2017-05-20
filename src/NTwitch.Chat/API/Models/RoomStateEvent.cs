using System.Linq;

namespace NTwitch.Chat.API
{
    internal class RoomStateEvent
    {
        // Parameters
        public string ChannelName { get; set; }

        // Tags
        public ulong RoomId { get; set; }
        public string BroadcastLanguage { get; set; } 
        public bool IsEmoteOnly { get; set; }
        public bool IsFollowersOnly { get; set; }
        public bool IsR9k { get; set; }
        public bool IsSlow { get; set; }
        public bool IsSubsOnly { get; set; }

        public static RoomStateEvent Create(ChatResponse msg)
        {
            var model = new RoomStateEvent();

            model.ChannelName = msg.Parameters.First();
            model.RoomId = ulong.Parse(msg.Tags["room-id"]);
            model.BroadcastLanguage = msg.Tags["broadcaster-lang"];     // Nullable
            model.IsEmoteOnly = msg.Tags["emote-only"] == "1";
            model.IsFollowersOnly = msg.Tags["followers-only"] == "1";
            model.IsR9k = msg.Tags["r9k"] == "1";
            model.IsSlow = msg.Tags["slow"] == "1";
            model.IsSubsOnly = msg.Tags["subs-only"] == "1";

            return model;
        }
    }
}
