namespace NTwitch.Rest
{
    public class RestUserFollow : RestFollow, IUserFollow
    {
        public IUser User { get; }

        internal RestUserFollow(TwitchRestClient client, ulong id) : base(client, id) { }
    }
}
