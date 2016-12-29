namespace NTwitch.Rest
{
    public class RestUserSubscription : RestSubscription, IUserSubscription
    {
        public RestUser User { get; }

        public RestUserSubscription(TwitchRestClient client, ulong id) : base(client, id) { }

        //IUserSubscription
        IUser IUserSubscription.User
            => User;
    }
}
