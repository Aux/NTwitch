using System.Threading.Tasks;

namespace NTwitch.Chat
{
    internal static class ChatChannelHelper
    {
        public static ChatSelfUser GetMyUser(TwitchChatClient client, ISimpleChannel channel)
        {
            var userState = client.Cache.GetUserState(channel.Name);
            return ChatSelfUser.Create(client, userState);
        }
        
        public static async Task SendMessageAsync(TwitchChatClient client, ISimpleChannel channel, string content, RequestOptions options = null)
        {
            await client.ApiClient.SendChannelMessageAsync(channel.Name, content, options).ConfigureAwait(false);
        }

        public static async Task ClearChatAsync(TwitchChatClient client, ISimpleChannel channel, string userName, string reason = null, uint? duration = null, RequestOptions options = null)
        {
            await client.ApiClient.ClearChatAsync(channel.Name, userName, reason, duration, options).ConfigureAwait(false);
        }
    }
}
