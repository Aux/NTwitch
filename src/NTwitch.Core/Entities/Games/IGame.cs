namespace NTwitch
{
    public interface IGame : IEntity
    {
        ulong GiantbombId { get; }
        string Name { get; }
        int Popularity { get; }
        TwitchImage Box { get; }
        TwitchImage Logo { get; }
    }
}
