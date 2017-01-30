using System.Threading.Tasks;

namespace NTwitch
{
    public interface ISelfUser : IEntity, IUser
    {
        /// <summary> Follow the specified channel. </summary>
        Task FollowAsync(uint channelId, bool notify = false);
        /// <summary> Unfollow the specified channel. </summary>
        Task UnfollowAsync(uint channelId);
        /// <summary> Get a collection of users this user has blocked. </summary>
        Task GetBlockedUsersAsync();
        /// <summary> Get a collection of streams this user is following. </summary>
        Task GetFollowedStreamsAsync();
        /// <summary> Get a collection of videos from channels this user is following. </summary>
        Task GetFollowedVideosAsync();
        /// <summary> Get a collection of channels this user is subscribed to. </summary>
        Task GetSubscriptionAsync(uint userId);
        /// <summary> Get a collection of channels this user is subscribed to. </summary>
        Task GetSubscriptionsAsync();
    }
}
