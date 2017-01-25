using System;
using System.Reflection;
using System.Threading.Tasks;

namespace NTwitch.Chat
{
    internal partial class ChatHandler
    {
        private TwitchChatClient _client;

        public ChatHandler(TwitchChatClient client)
        {
            _client = client;
            _client.Client.MessageReceived += OnMessageReceived;
        }

        public async Task OnMessageReceived(string msg)
        {
            var msgs = msg.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var m in msgs)
                await HandleAsync(m);
        }

        public async Task HandleAsync(string message)
        {
            TwitchMessage msg;
            try
            {
                msg = TwitchMessage.Parse(message);
            } catch (Exception ex)
            {
                await _client.Logger.ErrorAsync("Parser", ex);
                return;
            }
            
            switch (msg.Command)
            {
                case "PING":
                    await HandlePingAsync(msg); break;
                case "JOIN":
                    await HandleJoinAsync(msg); break;
                case "PART":
                    await HandlePartAsync(msg); break;
                case "MODE":
                    await HandleModeAsync(msg); break;
                case "NOTICE":
                    await HandleNoticeAsync(msg); break;
                case "PRIVMSG":
                    await HandlePrivMsgAsync(msg); break;
                case "CLEARCHAT":
                    await HandleClearChatAsync(msg); break;
                case "USERSTATE":
                    await HandleUserStateAsync(msg); break;
                case "RECONNECT":
                    await HandleReconnectAsync(msg); break;
                case "ROOMSTATE":
                    await HandleRoomStateAsync(msg); break;
                case "USERNOTICE":
                    await HandleUserNoticeAsync(msg); break;
                case "HOSTTARGET":
                    await HandleHostTargetAsync(msg); break;
                case "GLOBALUSERSTATE":
                    await HandleGlobalUserStateAsync(msg); break;
            }
        }

        public async Task HandlePingAsync(TwitchMessage message)
        {
            await _client.Client.SendAsync("PONG :tmi.twitch.tv").ConfigureAwait(false);
        }

        public async Task HandleJoinAsync(TwitchMessage message)
        {
            await Task.Delay(1);
        }

        public async Task HandlePartAsync(TwitchMessage message)
        {
            await Task.Delay(1);
        }

        public async Task HandleModeAsync(TwitchMessage message)
        {
            await Task.Delay(1);
        }

        public async Task HandleNoticeAsync(TwitchMessage message)
        {
            await Task.Delay(1);
        }

        public async Task HandleHostTargetAsync(TwitchMessage message)
        {
            await Task.Delay(1);
        }

        public async Task HandleClearChatAsync(TwitchMessage message)
        {
            await Task.Delay(1);
        }

        public async Task HandleUserStateAsync(TwitchMessage message)
        {
            await Task.Delay(1);
        }

        public async Task HandleReconnectAsync(TwitchMessage message)
        {
            await Task.Delay(1);
        }

        public async Task HandleRoomStateAsync(TwitchMessage message)
        {
            await Task.Delay(1);
        }
        
        public async Task HandleUserNoticeAsync(TwitchMessage message)
        {
            await Task.Delay(1);
        }

        public async Task HandlePrivMsgAsync(TwitchMessage message)
        {
            var msg = await ChatParser.ParseAsync<ChatMessage>(message, _client);
            await _client._messageReceivedEvent.InvokeAsync(msg).ConfigureAwait(false);
        }

        public async Task HandleGlobalUserStateAsync(TwitchMessage message)
        {
            await Task.Delay(1);
        }
    }
}
