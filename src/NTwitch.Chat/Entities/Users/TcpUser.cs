using NTwitch.Rest;

namespace NTwitch.Tcp
{
    public class TcpUser : TcpEntity<ulong>, IUser
    {
        public TcpUser(BaseRestClient client, ulong id) : base(client, id) { }
    }
}
