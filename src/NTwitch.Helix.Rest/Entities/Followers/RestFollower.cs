using System;
using Model = NTwitch.Helix.API.Follower;

namespace NTwitch.Helix.Rest
{
    public class RestFollower : RestEntity<ulong>
    {
        public ulong UserId { get; private set; }
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
