using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("NTwitch.Pubsub")]
namespace NTwitch.Rest
{
    public class RestPostPermissions : IPostPermissions
    {
        public bool? CanDelete { get; internal set; }
        public bool? CanModerate { get; internal set; }
        public bool? CanReply { get; internal set; }
    }
}
