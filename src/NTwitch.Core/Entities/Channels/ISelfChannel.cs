using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch
{
    public interface ISelfChannel : IChannel
    {
        string Email { get; }
        string StreamKey { get; }
        
        /// <summary> Creates a post in the channel feed. </summary> 
        /// <remarks> Required scope: channel_feed_edit </remarks>
        Task<IPost> CreatePostAsync(Action<CreatePostParams> args);
        /// <summary> Deletes a specified post in the channel feed. </summary> 
        /// <remarks> Required scope: channel_feed_edit </remarks>
        Task<IPost> DeletePostAsync(ulong postid);
        /// <summary> Updates specified properties of a specified channel. </summary> 
        /// <remarks> Required scope: channel_editor </remarks>
        Task<ISelfChannel> ModifyAsync(Action<ModifyChannelParams> args);
        /// <summary> Gets a list of users who are editors for a specified channel. </summary> 
        /// <remarks> Required scope: channel_read </remarks>
        Task<IEnumerable<IUser>> GetEditorsAsync();
        /// <summary> Gets a list of users subscribed to a specified channel, sorted by the date when they subscribed. </summary> 
        /// <remarks> Required scope: channel_subscriptions </remarks>
        Task<IEnumerable<IUserSubscription>> GetSubscribersAsync(SortDirection direction = SortDirection.Descending, TwitchPageOptions options = null);
        /// <summary> Checks if a specified channel has a specified user subscribed to it. </summary> 
        /// <remarks> Required scope: channel_check_subscription </remarks>
        Task<IUserSubscription> GetSubscriberAsync(ulong userid);
        /// <summary> Starts a commercial (advertisement) on a specified channel. </summary> 
        /// <remarks> Required scope: channel_editor </remarks>
        Task StartCommercialAsync(int duration = 30);
        /// <summary> Deletes the stream key for a specified channel. </summary> 
        /// <remarks> Required scope: channel_stream </remarks>
        Task<ISelfChannel> ResetStreamKeyAsync();
    }
}
