namespace NTwitch
{
    public interface IPostPermissions
    {
        bool? CanDelete { get; set; }
        bool? CanModerate { get; set; }
        bool? CanReply { get; set; }
    }
}
