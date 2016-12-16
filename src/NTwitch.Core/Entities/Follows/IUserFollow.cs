namespace NTwitch
{
    public interface IUserFollow : IFollow
    {
        IUser User { get; }
    }
}
