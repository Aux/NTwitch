using System.Collections.Generic;

namespace NTwitch.WebSocket
{
    public interface ICache
    {
        IReadOnlyCollection<IChannel> Channels { get; }
        IReadOnlyCollection<IUser> Users { get; }

        IChannel GetChannel(ulong channelId);
        IUser GetUser(ulong userId);

        void AddChannel(IChannel channel);
        void AddUser(IUser user);

        IChannel RemoveChannel(ulong channelId);
        IUser RemoveUser(ulong userId);
    }
}
