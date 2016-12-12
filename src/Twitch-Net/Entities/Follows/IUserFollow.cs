namespace Twitch
{
    public interface IUserFollow : IFollow
    {
        IUser User { get; }
    }
}
