using System;
using Model = NTwitch.Rest.API.BlockedUser;

namespace NTwitch.Rest
{
    public class RestBlockedUser : RestEntity<ulong>
    {
        /// <summary> The date and time this block was last updated </summary>
        public DateTime UpdatedAt { get; set; }
        /// <summary> The user associated with this block </summary>
        public RestUser User { get; set; }

        internal RestBlockedUser(TwitchRestClient client, ulong id) 
            : base(client, id) { }

        internal static RestBlockedUser Create(TwitchRestClient client, Model model)
        {
            var entity = new RestBlockedUser(client, model.Id);
            entity.Update(model);
            return entity;
        }

        internal virtual void Update(Model model)
        {
            UpdatedAt = model.UpdatedAt;
            User.Update(model.User);
        }
    }
}
