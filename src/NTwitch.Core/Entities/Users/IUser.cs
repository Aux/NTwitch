namespace NTwitch
{
    public interface IUser : IEntity<ulong>
    {
        string Name { get; }
    }
}
