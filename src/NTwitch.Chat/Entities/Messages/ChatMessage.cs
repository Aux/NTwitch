using NTwitch.Rest;
using System;
using System.Collections.Generic;

namespace NTwitch.Chat
{
    public class ChatMessage : MessageBase
    {
        [ChatProperty("id")]
        public string Id { get; private set; }
        [ChatProperty("emotes")]
        public string Emotes { get; private set; }
        [ChatProperty]
        public ChatChannel Channel { get; private set; }
        [ChatProperty]
        public ChatUser User { get; private set; }
        public string Content { get; private set; }

        internal ChatMessage(TwitchChatClient client) : base(client) { }

        internal static ChatMessage Create(BaseRestClient client, string msg)
        {
            var message = new ChatMessage(client as TwitchChatClient);
            ChatParser.PopulateObject(msg, message, client);
            return message;
        }
    }
}
