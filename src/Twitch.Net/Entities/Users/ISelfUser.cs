using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Twitch
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
        Task<IEnumerable<string>> GetEmotesAsync();
        /// <summary> Get a collection of stream objects that this user is following. </summary>
        /// <remarks> Authenticated, required scope `user_read` </remarks>
        Task GetStreamsAsync(int limit = 10, int page = 1, StreamType type = StreamType.All);
        /// <summary> Get a collection of videos from channels that this user is following. </summary>
        /// <remarks> Authenticated, required scope `user_read` </remarks>
        Task GetVideosAsync(int limit = 10, int page = 1, BroadcastType type = BroadcastType.All);
        Task AddFollow(string name);
        Task RemoveFollow(string name);
    }
}
