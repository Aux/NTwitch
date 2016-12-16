using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch
{
    public interface ISelfUser : IUser
    {
        /// <summary> The email for this user. </summary>
        string Email { get; }
        /// <summary> Indicates whether this user is partnered with Twitch. </summary>
        bool IsPartnered { get; }
        /// <summary> Indicates notification settings for this user. </summary>
        bool[] Notifications { get; }

        /// <summary> Get a collection of emoticons this user is authorized to use. </summary>
        /// <remarks> Authenticated, required scope `user_subscriptions` </remarks>
        Task<IEnumerable<Emoticon>> GetEmotesAsync();
        /// <summary> Get a collection of streams that this user is following. </summary>
        /// <remarks> Authenticated, required scope `user_read` </remarks>
        Task<IEnumerable<IStream>> GetStreamsAsync(StreamType type = StreamType.All, int limit = 10, int page = 1);
        /// <summary> Get a collection of videos from channels that this user is following. </summary>
        /// <remarks> Authenticated, required scope `user_read` </remarks>
        Task GetVideosAsync(BroadcastType type = BroadcastType.All, int limit = 10, int page = 1);
        Task SetNotificationAsync(string channel, bool notify);
        Task SetNotificationAsync(IChannel channel, bool notify);
        Task AddFollow(string channel);
        Task AddFollow(IChannel channel);
        Task RemoveFollow(string name);
        Task RemoveFollow(IChannel name);
        Task<IChannelSubscription> GetSubscriptionAsync(string name);
    }
}
