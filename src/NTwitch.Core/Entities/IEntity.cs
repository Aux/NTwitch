namespace NTwitch
{
    public interface IEntity<T>
    {
        ITwitchClient Client { get; }
        T Id { get; }
    }
}
