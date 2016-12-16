namespace NTwitch
{
    public interface IChannelFollow : IFollow
    {
        IChannel Channel { get; }
    }
}
