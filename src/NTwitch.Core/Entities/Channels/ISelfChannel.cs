namespace NTwitch
{
    public interface ISelfChannel : IChannel, IUpdateable
    {
        string Email { get; }
        string StreamKey { get; }
    }
}
