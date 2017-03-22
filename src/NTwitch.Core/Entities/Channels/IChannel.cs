namespace NTwitch
{
    public interface IChannel : IEntity<ulong>
    {
        string Name { get; }
        string DisplayName { get; }
    }
}
