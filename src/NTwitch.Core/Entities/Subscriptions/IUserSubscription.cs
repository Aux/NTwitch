namespace NTwitch
{
    public interface IUserSubscription : IEntity, ISubscription
    {
        IUser User { get; }
    }
}
