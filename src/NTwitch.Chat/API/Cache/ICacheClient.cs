using System;
using System.Collections.Generic;

namespace NTwitch.Chat
{
    public interface ICacheClient
    {
        IReadOnlyCollection<ChatSimpleUser> Users { get; }
        IReadOnlyCollection<ChatSimpleChannel> Channels { get; }
        IReadOnlyCollection<ChatMessage> Messages { get; }

        void AddUser(ChatSimpleUser user);
        void AddChannel(ChatSimpleChannel channel);
        void AddMessage(ChatMessage message);

        ChatSimpleUser RemoveUser(ulong userId);
        ChatSimpleChannel RemoveChannel(ulong channelId);
        ChatMessage RemoveMessage(string messageId);

        ChatSimpleUser GetUser(ulong userId);
        ChatSimpleChannel GetChannel(ulong channelId);
        ChatMessage GetMessage(string messageId);

        ChatSimpleUser GetOrAddUser(ulong userId, Func<ulong, ChatSimpleUser> userFactory);
        ChatSimpleChannel GetOrAddChannel(ulong channelId, Func<ulong, ChatSimpleChannel> channelFactory);
        ChatMessage GetOrAddMessage(string messageId, Func<string, ChatMessage> messageFactory);
    }
}
