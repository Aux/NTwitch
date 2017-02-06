using NTwitch.Chat;
using RogueCommands;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NTwitch.Tests
{
    public class CommandHandler
    {
        private TwitchChatClient _client;
        private CommandService _service;

        public async Task InitializeAsync(TwitchChatClient client)
        {
            _client = client;
            _service = new CommandService();

            await _service.AddModulesAsync(Assembly.GetEntryAssembly());
            
            _client.MessageReceived += HandleCommandAsync;
        }

        private async Task HandleCommandAsync(ChatMessage msg)
        {
            var context = new TwitchCommandContext(msg);

            int argPos = 0;
            if (msg.Content.HasStringPrefix("!", ref argPos))
            {
                var result = await _service.ExecuteAsync(context, msg.Content, argPos);

                if (!result.IsSuccess && result is ExecuteResult r)
                    await msg.Channel.SendMessageAsync(r.ToString());
            }
        }
    }
}
