namespace NTwitch
{
    public interface IEntity
    {
        ITwitchClient Client { get; }
        uint Id { get; }
    }
}
