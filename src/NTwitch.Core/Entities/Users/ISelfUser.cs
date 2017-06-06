using System.Threading.Tasks;

namespace NTwitch
{
    public interface ISelfUser : IUser, IUpdateable
    {
        string Email { get; }
        bool IsVerified { get; }
        bool IsPartner { get; }
        bool IsTwitterConnected { get; }
        //IUserNotifications Notifications { get; }
    }
}
