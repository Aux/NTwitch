using System;
using System.Collections.Generic;

namespace NTwitch
{
    public interface ICacheClient
    {
        IReadOnlyCollection<IUser> Users { get; }
        IReadOnlyCollection<IChannel> Channels { get; }
        IReadOnlyCollection<IMessage> Messages { get; }

        void AddUser(IUser user);
        void AddChannel(IChannel channel);
        void AddMessage(IMessage message);

        IUser RemoveUser(ulong userId);
        IChannel RemoveChannel(ulong channelId);
        IMessage RemoveMessage(string messageId);

        IUser GetUser(ulong userId);
        IChannel GetChannel(ulong channelId);
        IMessage GetMessage(string messageId);

        IUser GetOrAddUser(ulong userId, Func<ulong, IUser> userFactory);
        IChannel GetOrAddChannel(ulong channelId, Func<ulong, IChannel> channelFactory);
        IMessage GetOrAddMessage(string messageId, Func<string, IMessage> messageFactory);
    }
}
