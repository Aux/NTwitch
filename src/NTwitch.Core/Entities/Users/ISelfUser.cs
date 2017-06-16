using System.Threading.Tasks;

namespace NTwitch
{
    public interface ISelfUser : IUser, IUpdateable
    {
        string Email { get; }
        //IUserNotifications Notifications { get; }
    }
}
