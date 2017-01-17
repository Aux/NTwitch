using NTwitch.Rest;

namespace NTwitch.Chat
{
    public class ChatMessage : MessageBase
    {
        [ChatProperty("id")]
        public string Id { get; internal set; }
        [ChatProperty("emotes")]
        public string Emotes { get; internal set; }
        [ChatProperty]
        public ChatChannel Channel { get; internal set; }
        [ChatProperty]
        public ChatUser User { get; internal set; }
        [ChatValueBetween(" :", null)]
        public string Content { get; internal set; }

        internal ChatMessage(TwitchChatClient client) : base(client) { }

        internal static ChatMessage Create(BaseRestClient client, string msg)
        {
            var message = new ChatMessage(client as TwitchChatClient);
            ChatParser.PopulateObject(msg, message, client);
            return message;
        }
    }
}
