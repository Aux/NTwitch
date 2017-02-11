namespace NTwitch
{
    public interface IChannelFollow : IEntity<ulong>, IFollow
    {
        IChannel Channel { get; }
    }
}
