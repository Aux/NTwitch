namespace NTwitch.Helix
{
    public interface IUser : IEntity<ulong>
    {
        string Name { get; }
        string Username { get; }
    }
}
