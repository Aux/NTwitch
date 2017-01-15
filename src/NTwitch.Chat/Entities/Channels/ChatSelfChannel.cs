using NTwitch.Rest;

namespace NTwitch.Chat
{
    public class ChatSelfChannel : ChatChannel
    {
        public ChatSelfChannel(TwitchChatClient client) : base(client) { }

        public static new ChatSelfChannel Create(BaseRestClient client, string msg)
        {
            var channel = new ChatSelfChannel(client as TwitchChatClient);
            ChatParser.PopulateObject(msg, channel, client);
            return channel;
        }
    }
}
