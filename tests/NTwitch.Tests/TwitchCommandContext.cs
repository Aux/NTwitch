using NTwitch.Chat;
using RogueCommands;
using System.Threading.Tasks;

namespace NTwitch.Tests
{
    public class TwitchCommandContext : ICommandContext
    {
        public ChatMessage Message { get; }
        public ChatChannel Channel { get; }
        public ChatUser User { get; }

        public TwitchCommandContext(ChatMessage msg)
        {
            Message = msg;
            Channel = msg.Channel;
            User = msg.User;
        }

        public Task ReplyAsync(string content)
            => Channel.SendMessageAsync(content);
    }
}
