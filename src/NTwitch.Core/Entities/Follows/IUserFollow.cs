namespace NTwitch
{
    public interface IUserFollow : IEntity, IFollow
    {
        IUser User { get; }
    }
}
