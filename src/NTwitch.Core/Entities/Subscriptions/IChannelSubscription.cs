namespace NTwitch
{
    public interface IChannelSubscription : IEntity, ISubscription
    {
        IChannel Channel { get; }
    }
}
