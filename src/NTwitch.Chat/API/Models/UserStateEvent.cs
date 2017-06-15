using NTwitch.Chat.Queue;
using System.Linq;

namespace NTwitch.Chat.API
{
    internal class UserStateEvent
    {
        // Tags
        public string Badges { get; set; }
        public string Color { get; set; }
        public string DisplayName { get; set; }
        public string EmoteSets { get; set; }           // Probably goes in selfuser
        public bool IsMod { get; set; }
        public bool IsSubscriber { get; set; }
        public string UserType { get; set; }

        // Parameters
        public string ChannelName { get; set; } // First

        internal static UserStateEvent Create(ChatResponse msg)
        {
            var entity = new UserStateEvent();
            entity.Update(msg);
            return entity;
        }

        internal void Update(ChatResponse msg)
        {
            Badges = msg.Tags["badges"];
            Color = msg.Tags["color"];
            DisplayName = msg.Tags["display-name"];
            EmoteSets = msg.Tags["emote-sets"];
            IsMod = msg.Tags["mod"] == "1";
            IsSubscriber = msg.Tags["subscriber"] == "1";
            UserType = msg.Tags["user-type"];
            
            ChannelName = msg.Parameters.First().Substring(1).Trim();
        }
    }
}
