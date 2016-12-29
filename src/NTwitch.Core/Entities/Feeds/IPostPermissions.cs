namespace NTwitch
{
    public interface IPostPermissions
    {
        bool? CanDelete { get; }
        bool? CanModerate { get; }
        bool? CanReply { get; }
    }
}
