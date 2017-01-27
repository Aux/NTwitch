using System.Threading.Tasks;

namespace NTwitch
{
    public interface IChannel : IEntity
    {
        /// <summary> The name of this channel. </summary>
        string Name { get; }

        /// <summary> Follow this channel. </summary>
        Task FollowAsync(bool notify = false);
        /// <summary> Unfollow this channel. </summary>
        Task UnfollowAsync();
        /// <summary> Get a collection of posts for this channel. </summary>
        Task GetPostsAsync();
        /// <summary> Get a specific post for this channel. </summary>
        Task GetPostAsync();
        /// <summary> Get a collection of users that follow this channel. </summary>
        Task GetFollowersAsync();
        /// <summary> Get a collection of teams this channel is a member of. </summary>
        Task GetTeamsAsync();
        /// <summary> Get a collection of videos for this channel. </summary>
        Task GetVideosAsync();
        /// <summary> Get the stream object for this channel, if online. </summary>
        Task GetStreamAsync();
        /// <summary> Get a collection of the top rated clips for this channel. </summary>
        Task GetTopClipsAsync(string game, VideoPeriod period = VideoPeriod.Week, bool istrending = false, PageOptions options = null);
        /// <summary> Get a specific clip for this channel. </summary>
        Task GetClipAsync(string clipId);
    }
}
