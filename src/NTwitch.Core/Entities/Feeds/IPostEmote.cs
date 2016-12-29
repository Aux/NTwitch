namespace NTwitch
{
    public interface IPostEmote : IEntity
    {
        int End { get; }
        ulong SetId { get; }
        int Start { get; }
    }
}
