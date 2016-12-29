namespace NTwitch.Rest
{
    public class RestPostPermissions : IPostPermissions
    {
        public bool? CanDelete { get; }
        public bool? CanModerate { get; }
        public bool? CanReply { get; }
    }
}
