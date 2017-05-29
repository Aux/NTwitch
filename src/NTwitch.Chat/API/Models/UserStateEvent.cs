using System.Linq;

namespace NTwitch.Chat.API
{
    internal class UserStateEvent
    {
        // Parameters
        public string ChannelName { get; set; }
        
        // Tags
        public string Badges { get; set; }          // string/int
        public string Color { get; set; }           // #string
        public string EmoteSets { get; set; }       // int,int,int,int
        public string DisplayName { get; set; }
        public string UserType { get; set; }
        public bool IsMod { get; set; }
        public bool IsSubscriber { get; set; }

        public static UserStateEvent Create(ChatResponse msg)
        {
            var model = new UserStateEvent();

            model.ChannelName = msg.Parameters.First();
            model.Badges = msg.Tags["badges"];          // Nullable
            model.Color = msg.Tags["color"];
            model.EmoteSets = msg.Tags["emote-sets"];   // Nullable
            model.UserType = msg.Tags["user-type"];
            model.IsMod = msg.Tags["mod"] == "1";
            model.IsSubscriber = msg.Tags["subscriber"] == "1";

            return model;
        }
    }
}
