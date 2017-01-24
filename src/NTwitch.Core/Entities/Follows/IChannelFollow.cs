namespace NTwitch
{
    public interface IChannelFollow : IEntity, IFollow
    {
        IChannel Channel { get; }
    }
}
