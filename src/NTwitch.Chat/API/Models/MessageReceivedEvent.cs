using NTwitch.Chat.Queue;
using System;
using System.Linq;

namespace NTwitch.Chat.API
{
    internal class MessageReceivedEvent
    {
        // Tags
        // Message
        public string Id { get; set; }
        public DateTime SentTimestamp { get; set; }
        public DateTime TmiSentTimestamp { get; set; }
        public string Badges { get; set; }
        public string Emotes { get; set; }

        // Channel
        public ulong ChannelId { get; set; }
        
        // User
        public ulong UserId { get; set; }
        public string Color { get; set; }
        public string DisplayName { get; set; }
        public string UserType { get; set; }
        public bool IsMod { get; set; }
        public bool IsSubscriber { get; set; }
        public bool IsTurbo { get; set; }

        // Parameters
        public string ChannelName { get; set; } // First
        public string Content { get; set; } // Last

        internal static MessageReceivedEvent Create(ChatResponse msg)
        {
            var entity = new MessageReceivedEvent();
            entity.Update(msg);
            return entity;
        }

        internal void Update(ChatResponse msg)
        {
            Id = msg.Tags["id"];

            if (msg.Tags.TryGetValue("sent-ts", out string sentTs))
                SentTimestamp = DateTimeHelper.GetDateTime(sentTs);

            TmiSentTimestamp = DateTimeHelper.GetDateTime(msg.Tags["tmi-sent-ts"]);
            Badges = msg.Tags["badges"];
            Emotes = msg.Tags["emotes"];

            ChannelId = ulong.Parse(msg.Tags["room-id"]);

            UserId = ulong.Parse(msg.Tags["user-id"]);
            Color = msg.Tags["color"];
            DisplayName = msg.Tags["display-name"];
            UserType = msg.Tags["user-type"];
            IsMod = msg.Tags["mod"] == "1";
            IsSubscriber = msg.Tags["subscriber"] == "1";
            IsTurbo = msg.Tags["turbo"] == "1";

            ChannelName = msg.Parameters.First().Substring(1);
            Content = msg.Parameters.Last().Trim();
        }
    }
}
