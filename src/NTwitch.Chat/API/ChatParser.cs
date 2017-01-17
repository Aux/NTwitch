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
            var msgs = msg.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var m in msgs)
                await ParseAsync(m);
        }

        public async Task ParseAsync(string msg)
        {
            if (msg == "PING :tmi.twitch.tv")
            {
                await HandlePingAsync(msg).ConfigureAwait(false);
                return;
            }

            string content = msg.Split(new[] { ' ' }, 2)[1];
            int startIndex = content.IndexOf("twitch.tv ") + 10; // 10 chars for `twitch.tv `
            if (startIndex < 0)
            {
                startIndex = content.IndexOf("jtv ") + 5; // 5 chars for `.jtv `
                if (startIndex < 0)
                {
                    await _client.Logger.ErrorAsync("Chat", new InvalidOperationException(msg));
                    return;
                }
            }

            int endIndex = content.Substring(startIndex).IndexOf(' ');

            string type = content.Substring(startIndex, endIndex);
            
            switch (type)
            {
                case "JOIN":
                    await HandleJoinAsync(msg).ConfigureAwait(false); break;
                case "PART":
                    await HandlePartAsync(msg).ConfigureAwait(false); break;
                case "MODE":
                    await HandleModeAsync(msg).ConfigureAwait(false); break;
                case "NOTICE":
                    await HandleNoticeAsync(msg).ConfigureAwait(false); break;
                case "PRIVMSG":
                    await HandlePrivMsgAsync(msg).ConfigureAwait(false); break;
                case "CLEARCHAT":
                    await HandleClearChatAsync(msg).ConfigureAwait(false); break;
                case "USERSTATE":
                    await HandleUserStateAsync(msg).ConfigureAwait(false); break;
                case "RECONNECT":
                    await HandleReconnectAsync(msg).ConfigureAwait(false); break;
                case "ROOMSTATE":
                    await HandleRoomStateAsync(msg).ConfigureAwait(false); break;
                case "USERNOTICE":
                    await HandleUserNoticeAsync(msg).ConfigureAwait(false); break;
                case "HOSTTARGET":
                    await HandleHostTargetAsync(msg).ConfigureAwait(false); break;
                case "GLOBALUSERSTATE":
                    await HandleGlobalUserStateAsync(msg).ConfigureAwait(false); break;
                default:
                    await _client.Logger.ErrorAsync(type, new NotSupportedException(msg)); break;
            }
        }

        public async Task HandlePingAsync(string msg)
        {
            await _client.Client.SendAsync("PONG :tmi.twitch.tv");
        }

        public async Task HandleJoinAsync(string msg)
        {
            var channel = new ChatChannel(_client);
            PopulateObject(msg, channel, _client);
            await _client._joinedChannelEvent.InvokeAsync(channel);
        }

        public async Task HandlePartAsync(string msg)
        {
            var channel = new ChatChannel(_client);
            PopulateObject(msg, channel, _client);
            await _client._leftChannelEvent.InvokeAsync(channel);
        }

        public async Task HandleModeAsync(string msg)
        {
            await _client._userUpdatedEvent.InvokeAsync();
        }

        public async Task HandleNoticeAsync(string msg)
        {
            if (msg.Contains("Login authentication failed"))
                throw new UnauthorizedAccessException("Login authentication failed");

            await _client._noticeReceivedEvent.InvokeAsync();
        }

        public async Task HandleHostTargetAsync(string msg)
        {
            await Task.Delay(1);
        }

        public async Task HandleClearChatAsync(string msg)
        {
            await Task.Delay(1);
        }

        public async Task HandleUserStateAsync(string msg)
        {
            await Task.Delay(1);
        }

        public async Task HandleReconnectAsync(string msg)
        {
            await Task.Delay(1);
        }

        public async Task HandleRoomStateAsync(string msg)
        {
            await Task.Delay(1);
        }
        
        public async Task HandleUserNoticeAsync(string msg)
        {
            await Task.Delay(1);
        }

        public async Task HandlePrivMsgAsync(string msg)
        {
            var message = new ChatMessage(_client);
            PopulateObject(msg, message, _client);
            await _client._messageReceivedEvent.InvokeAsync(message);
        }

        public async Task HandleGlobalUserStateAsync(string msg)
        {
            await Task.Delay(1);
        }
    }
}
