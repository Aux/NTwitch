using System;
using System.Threading.Tasks;

namespace NTwitch.Chat
{
    internal partial class ChatParser
    {
        private TwitchChatClient _client;

        public ChatParser(TwitchChatClient client)
        {
            _client = client;
            _client.Client.MessageReceived += OnMessageReceived;
        }

        public async Task OnMessageReceived(string msg)
        {
            if (msg == "PING :tmi.twitch.tv")
            {
                await HandlePing(msg).ConfigureAwait(false);
                return;
            }

            string content = msg.Split(new[] { ' ' }, 2)[1];
            int startIndex = content.IndexOf("twitch.tv ") + 10; // 10 chars for `twitch.tv `
            if (startIndex < 0)
            {
                startIndex = content.IndexOf(".jtv ") + 5; // 5 chars for `.jtv `
                if (startIndex < 0)
                    throw new InvalidOperationException();
            }

            int endIndex = content.Substring(startIndex).IndexOf(' ');

            string type = content.Substring(startIndex, endIndex);

            switch (type)
            {
                case "JOIN":
                    await HandleJoin(msg).ConfigureAwait(false); break;
                case "PART":
                    await HandlePart(msg).ConfigureAwait(false); break;
                case "MODE":
                    await HandleMode(msg).ConfigureAwait(false); break;
                case "NOTICE":
                    await HandleNotice(msg).ConfigureAwait(false); break;
                case "PRIVMSG":
                    await HandlePrivMsg(msg).ConfigureAwait(false); break;
                case "CLEARCHAT":
                    await HandleClearChat(msg).ConfigureAwait(false); break;
                case "USERSTATE":
                    await HandleUserState(msg).ConfigureAwait(false); break;
                case "RECONNECT":
                    await HandleReconnect(msg).ConfigureAwait(false); break;
                case "ROOMSTATE":
                    await HandleRoomState(msg).ConfigureAwait(false); break;
                case "USERNOTICE":
                    await HandleUserNotice(msg).ConfigureAwait(false); break;
                case "HOSTTARGET":
                    await HandleHostTarget(msg).ConfigureAwait(false); break;
                case "GLOBALUSERSTATE":
                    await HandleGlobalUserState(msg).ConfigureAwait(false); break;
                default:
                    Console.WriteLine("Unsupported type: " + type); break;
                    //throw new NotSupportedException("The message type `" + type + "` is not supported at this time.");
            }
        }

        public async Task HandlePing(string msg)
        {
            await _client.Client.SendAsync("PONG :tmi.twitch.tv");
        }

        public async Task HandleJoin(string msg)
        {
            await _client._joinedChannelEvent.InvokeAsync();
            Console.WriteLine(msg.Trim());
        }

        public async Task HandlePart(string msg)
        {
            await Task.Delay(1);
        }

        public async Task HandleMode(string msg)
        {
            await Task.Delay(1);
        }

        public async Task HandleNotice(string msg)
        {
            await Task.Delay(1);
        }

        public async Task HandleHostTarget(string msg)
        {
            await Task.Delay(1);
        }

        public async Task HandleClearChat(string msg)
        {
            await Task.Delay(1);
        }

        public async Task HandleUserState(string msg)
        {
            await Task.Delay(1);
        }

        public async Task HandleReconnect(string msg)
        {
            await Task.Delay(1);
        }

        public async Task HandleRoomState(string msg)
        {
            await Task.Delay(1);
        }
        
        public async Task HandleUserNotice(string msg)
        {
            await Task.Delay(1);
        }

        public async Task HandlePrivMsg(string msg)
        {
            var message = new ChatMessage(_client);
            PopulateObject(msg, message, _client);
            await _client._messageReceivedEvent.InvokeAsync(message);
        }

        public async Task HandleGlobalUserState(string msg)
        {
            await Task.Delay(1);
        }
    }
}
