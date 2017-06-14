using System.Threading.Tasks;

namespace NTwitch
{
    public interface ISelfUser : IUser, IUpdateable
    {
        string Email { get; }
        bool IsVerified { get; }
        bool IsPartnered { get; }
        bool IsTwitterConnected { get; }
        //IUserNotifications Notifications { get; }
    }
}
