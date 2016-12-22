using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch
{
    public interface ISelfChannel : IChannel
    {
        string Email { get; }
        string StreamKey { get; }

        /// <summary> Updates specified properties of this channel. </summary>
        /// <remarks> 
        /// Required scope to update delay or channel_feed_enabled parameter: a channel_editor token from the channel owner
        /// Required scope to update other parameters: channel_editor 
        /// </remarks>
        Task ModifyAsync(Action<ModifyChannelParams> action);
        /// <summary> Gets a list of users who are editors for this channel. </summary>
        /// <remarks> Required scope: channel_read </remarks>
        Task GetEditorsAsync();
        /// <summary> Gets a list of users subscribed to this channel, sorted by the date when they subscribed. </summary>
        /// <remarks> Required scope: channel_subscriptions </remarks>
        Task GetSubscribersAsync(bool descending = true, TwitchPageOptions page = null);
        /// <summary> Returns a subscription object which includes the user if that user is subscribed. </summary>
        /// <remarks> Required scope: channel_check_subscription </remarks>
        Task GetSubscriberAsync(uint id);
        /// <summary>  </summary>
        /// <remarks>  </remarks>
        Task StartCommercialAsync();
        /// <summary>  </summary>
        /// <remarks>  </remarks>
        Task ResetStreamKeyAsync();
    }
}
