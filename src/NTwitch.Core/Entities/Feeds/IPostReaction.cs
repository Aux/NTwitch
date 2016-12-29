namespace NTwitch
{
    public interface IPostReaction
    {
        string Name { get; }
        int Count { get; }
        string EmoteName { get; }
        ulong[] UserIds { get; }
    }
}
