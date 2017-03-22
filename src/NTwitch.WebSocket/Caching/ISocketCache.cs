using System.Collections.Generic;

namespace NTwitch.WebSocket
{
    public interface ISocketCache
    {
        IReadOnlyCollection<SocketChannel> Channels { get; }
        IReadOnlyCollection<SocketUser> Users { get; }

        SocketChannel GetChannel(ulong channelId);
        SocketUser GetUser(ulong userId);

        void AddChannel(SocketChannel channel);
        void AddUser(SocketUser user);

        SocketChannel RemoveChannel(ulong channelId);
        SocketUser RemoveUser(ulong userId);
    }
}
