namespace NTwitch
{
    public interface IChannelSummary : IEntity
    {
        string DisplayName { get; }
        string Name { get; }
    }
}
