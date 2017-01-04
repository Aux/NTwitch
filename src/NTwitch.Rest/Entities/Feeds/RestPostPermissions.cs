using System.Runtime.CompilerServices;


namespace NTwitch.Rest
{
    public class RestPostPermissions : IPostPermissions
    {
        public bool? CanDelete { get; internal set; }
        public bool? CanModerate { get; internal set; }
        public bool? CanReply { get; internal set; }
    }
}
