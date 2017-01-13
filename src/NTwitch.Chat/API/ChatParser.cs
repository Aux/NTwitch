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
        }

        public async Task OnMessageReceived(string msg)
        {
            if (msg == "PING :tmi.twitch.tv")
            {
                await HandlePing(msg).ConfigureAwait(false);
                return;
            }
            
            string content = msg.Split(' ')[1];

            int startIndex = content.IndexOf(":tmi.twitch.tv ");
            if (startIndex < 0)
            {
                startIndex = content.IndexOf(":jtv ");
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
                    throw new NotSupportedException("The message type `" + type + "` is not supported at this time.");
            }
        }

        public async Task HandlePing(string msg)
        {
            await _client.Client.SendAsync("PONG :tmi.twitch.tv");
        }

        public async Task HandleJoin(string msg)
        {
            await Task.Delay(1);
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
            await Task.Delay(1);
        }

        public async Task HandleGlobalUserState(string msg)
        {
            await Task.Delay(1);
        }
    }
}
