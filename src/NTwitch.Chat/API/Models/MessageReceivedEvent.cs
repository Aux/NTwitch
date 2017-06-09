namespace NTwitch.Chat.API
{
    internal class MessageReceivedEvent
    {
        // Tags
        public string Badges { get; set; }
        public string Color { get; set; }
        public string DisplayName { get; set; }
        public string Emotes { get; set; }
        public string Id { get; set; }
        public string Mod { get; set; }
        public string RoomId { get; set; }
        public string SentTimestamp { get; set; }
        public string Subscriber { get; set; }
        public string TmiSentTimestamp { get; set; }
        public string Turbo { get; set; }
        public string UserId { get; set; }
        public string UserType { get; set; }

        // Parameters
        public string ChannelName { get; set; } // First
        public string Content { get; set; } // Last
    }
}
