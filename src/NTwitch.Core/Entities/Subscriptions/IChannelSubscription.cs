namespace NTwitch
{
    public interface IChannelSubscription : IEntity<ulong>, ISubscription
    {
        IChannel Channel { get; }
    }
}
