using System.Collections.Generic;

namespace NTwitch.WebSocket
{
    public interface ISocketCache : ICache
    {
        new IReadOnlyCollection<SocketChannel> Channels { get; }
        new IReadOnlyCollection<SocketUser> Users { get; }

        new SocketChannel GetChannel(ulong channelId);
        new SocketUser GetUser(ulong userId);

        void AddChannel(SocketChannel channel);
        void AddUser(SocketUser user);

        new SocketChannel RemoveChannel(ulong channelId);
        new SocketUser RemoveUser(ulong userId);
    }
}
