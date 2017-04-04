using System;
using System.Collections.Generic;

namespace NTwitch.Chat.API
{
    internal class Message
    {
        // From tags
        public Channel Channel { get; set; }
        // From tags
        public User Author { get; set; }
        // tmi-sent-ts tag
        public DateTime Timestamp { get; set; }
        // id tag
        public string Id { get; set; }
        // Final parameter
        public string Content { get; set; }
        // emotes tag
        public IEnumerable<Emote> Emotes { get; set; }
    }
}
