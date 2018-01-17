namespace NTwitch.Helix
{
    public interface IUser : IEntity<ulong>, IUpdateable
    {
        string Name { get; }
        string Username { get; }
    }
}
