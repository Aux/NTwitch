using System;

namespace NTwitch.Chat
{
    public class ChatMessage : ChatEntity, IMessage
    {
        public DateTime UtcTimestamp { get; } = DateTime.UtcNow;
        [ChatProperty("id")]
        public new string Id { get; internal set; }
        [ChatProperty("emotes")]
        public string Emotes { get; internal set; }
        [ChatProperty(PropertyType.Complex)]
        public ChatChannel Channel { get; internal set; }
        [ChatProperty(PropertyType.Complex)]
        public ChatUser User { get; internal set; }
        [ChatProperty(PropertyType.Content)]
        public string Content { get; internal set; }
        
        internal ChatMessage(TwitchChatClient client) : base(client) { }
    }
}
