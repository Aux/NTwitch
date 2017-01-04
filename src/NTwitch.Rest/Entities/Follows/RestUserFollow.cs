using System.Runtime.CompilerServices;


namespace NTwitch.Rest
{
    public class RestUserFollow : RestFollow, IUserFollow
    {
        public IUser User { get; internal set; }

        internal RestUserFollow(ITwitchClient client) : base(client) { }
    }
}
