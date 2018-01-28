using System;
using Model = NTwitch.Helix.API.Follower;

namespace NTwitch.Helix.Rest
{
    public class RestFollower : RestEntity<ulong>
    {
        /// <summary> The follower's id </summary>
        public ulong UserId { get; private set; }
        /// <summary> The date and time this follow was created </summary>
        public DateTime FollowedAt { get; private set; }

        internal RestFollower(BaseTwitchClient twitch, ulong id)
            : base(twitch, id) { }
        internal static RestFollower Create(BaseTwitchClient twitch, Model model)
        {
            var entity = new RestFollower(twitch, model.Id);
            entity.Update(model);
            return entity;
        }
        internal virtual void Update(Model model)
        {
            UserId = model.UserId;
            FollowedAt = model.FollowedAt;
        }
    }
}
