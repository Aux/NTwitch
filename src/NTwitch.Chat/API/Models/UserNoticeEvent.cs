using NTwitch.Chat.Queue;
using System;
using System.Linq;

namespace NTwitch.Chat.API
{
    internal class UserNoticeEvent
    {
        // Tags
        // Message
        public string Id { get; set; }
        public DateTime TmiSentTimestamp { get; set; }
        public string Badges { get; set; }
        public string Emotes { get; set; }

        // Channel
        public ulong ChannelId { get; set; }

        // User
        public ulong UserId { get; set; }
        public string DisplayName { get; set; }
        public string Name { get; set; }
        public string UserType { get; set; }
        public string Color { get; set; }
        public bool IsMod { get; set; }
        public bool IsTurbo { get; set; }

        // Notice
        public string Type { get; set; }
        public int Months { get; set; }
        public string PlanName { get; set; }
        public string Plan { get; set; }
        public string SystemMessage { get; set; }

        // Parameters
        public string ChannelName { get; set; }  // First
        public string Content { get; set; }      // Last
        
        internal static UserNoticeEvent Create(ChatResponse msg)
        {
            var entity = new UserNoticeEvent();
            entity.Update(msg);
            return entity;
        }

        internal void Update(ChatResponse msg)
        {
            Id = msg.Tags["id"];
            TmiSentTimestamp = DateTimeHelper.GetDateTime(msg.Tags["tmi-sent-ts"]);
            Badges = msg.Tags["badges"];
            Emotes = msg.Tags["emotes"];

            ChannelId = ulong.Parse(msg.Tags["room-id"]);

            UserId = ulong.Parse(msg.Tags["user-id"]);
            DisplayName = msg.Tags["display-name"];
            Name = msg.Tags["login"];
            UserType = msg.Tags["user-type"];
            Color = msg.Tags["color"]?.Substring(1);
            IsMod = msg.Tags["mod"] == "1";
            IsTurbo = msg.Tags["turbo"] == "1";

            Type = msg.Tags["msg-id"];
            Months = int.Parse(msg.Tags["msg-param-months"]);
            PlanName = msg.Tags["msg-param-sub-plan-name"].Replace(@"\s", " ");
            Plan = msg.Tags["msg-param-sub-plan"];
            SystemMessage = msg.Tags["system-msg"].Replace(@"\s", " ");

            ChannelName = msg.Parameters.First().Substring(1);
            Content = msg.Parameters.Last();
        }
    }
}
