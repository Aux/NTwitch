using System.Collections.Generic;

namespace NTwitch
{
    public interface ICache
    {
        IReadOnlyCollection<IChannel> Channels { get; }
        IReadOnlyCollection<IUser> Users { get; }
        
        IReadOnlyCollection<T> GetChannels<T>() where T : IChannel;
        IReadOnlyCollection<T> GetUsers<T>() where T : IUser;
        
        IChannel GetChannel(ulong channelId);
        IUser GetUser(ulong userId);
        
        void AddChannel(IChannel channel);
        void AddUser(IUser user);
        
        IChannel RemoveChannel(ulong channelId);
        IUser RemoveUser(ulong userId);
    }
}
