namespace NTwitch
{
    public interface IUserSubscription : IEntity<ulong>, ISubscription
    {
        IUser User { get; }
    }
}
