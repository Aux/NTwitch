namespace Twitch
{
    public interface IChannelFollow : IFollow
    {
        IChannel Channel { get; }
    }
}
