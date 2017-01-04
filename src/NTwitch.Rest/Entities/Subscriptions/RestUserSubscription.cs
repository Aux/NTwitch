using System.Runtime.CompilerServices;


namespace NTwitch.Rest
{
    public class RestUserSubscription : RestSubscription, IUserSubscription
    {
        public RestUser User { get; internal set; }

        public RestUserSubscription(ITwitchClient client) : base(client) { }

        //IUserSubscription
        IUser IUserSubscription.User
            => User;
    }
}
