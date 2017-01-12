using NTwitch.Rest;

namespace NTwitch.Chat
{
    public class ChatUserMessage : ChatMessage
    {
        [ChatProperty]
        public ChatUser User { get; private set; }

        internal ChatUserMessage(TwitchChatClient client) : base(client) { }

        internal static new ChatUserMessage Create(BaseRestClient client, string msg)
        {
            var message = new ChatUserMessage(client as TwitchChatClient);
            ChatParser.PopulateObject(msg, message);
            return message;
        }
    }
}
