using RogueCommands;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch.Tests.Modules
{
    public class TestModule : ModuleBase<TwitchCommandContext>
    {
        [Command("echo")]
        public Task EchoAsync(string content)
            => Context.ReplyAsync(content);
        
        [Group("channel")]
        public class InfoModule : ModuleBase<TwitchCommandContext>
        {
            [Command("id")]
            public Task IdAsync()
                => Context.ReplyAsync(Context.Channel.Id.ToString());

            [Command("name")]
            public Task NameAsync()
                => Context.ReplyAsync(Context.Channel.Name);

            [Command("followers")]
            public async Task FollowersAsync()
                => await Context.ReplyAsync((await Context.Channel.GetFollowersAsync()).Count().ToString());
        }
    }
}
