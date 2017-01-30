using System.Threading.Tasks;

namespace NTwitch
{
    public interface ISelfChannel : IEntity, IChannel
    {
        /// <summary> Modify a property of this channel. </summary>
        Task ModifyAsync();
        /// <summary> Create a new post on this channel. </summary>
        Task CreatePostAsync();
        /// <summary> Delete an existing post on this channel. </summary>
        Task DeletePostAsync(uint postId);
        /// <summary> Reset the stream key on this channel. </summary>
        Task ResetStreamKeyAsync();
        /// <summary> Start a commercial on this channel. </summary>
        Task StartCommercialAsync(int duration = 30);
        /// <summary> Get a collection of users who have editor permissions on this channel. </summary>
        Task GetEditorsAsync();
        /// <summary> Get a collection of users who are subscribed to this channel. </summary>
        Task GetSubscribersAsync();
        /// <summary> Get a specified subscriber, if it exists. </summary>
        Task GetSubscriberAsync(uint userId);
    }
}
