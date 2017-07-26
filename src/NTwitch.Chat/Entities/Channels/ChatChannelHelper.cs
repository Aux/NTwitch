using System;
using System.Threading.Tasks;

namespace NTwitch.Chat
{
    internal static class ChatChannelHelper
    {
        public static ChatSelfUser GetMyUser(TwitchChatClient client, ChatSimpleChannel channel)
        {
            var userState = client.Cache.GetUserState(channel.Name);
            return ChatSelfUser.Create(client, userState);
        }
        
        public static async Task SendMessageAsync(TwitchChatClient client, string channelName, string content, RequestOptions options = null)
        {
            await client.ApiClient.SendChannelMessageAsync(channelName, content, options).ConfigureAwait(false);
        }
    }
}
