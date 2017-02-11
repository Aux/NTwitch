using System;

namespace NTwitch.Chat
{
    public class ChatMessage : ChatEntity<string>, IMessage
    {
        public DateTime UtcTimestamp { get; } = DateTime.UtcNow;
        [ChatProperty("emotes")]
        public string Emotes { get; internal set; }
        [ChatProperty("bits")]
        public int BitsTotal { get; internal set; } = 0;
        [ChatProperty(PropertyType.Content)]
        public string Content { get; internal set; }
        [ChatProperty(PropertyType.Complex)]
        public ChatChannel Channel { get; internal set; }
        [ChatProperty(PropertyType.Complex)]
        public ChatUser User { get; internal set; }

        internal ChatMessage(TwitchChatClient client) : base(client) { }
    }
}
