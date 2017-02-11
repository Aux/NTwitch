namespace NTwitch
{
    public interface IUserFollow : IEntity<ulong>, IFollow
    {
        IUser User { get; }
    }
}
