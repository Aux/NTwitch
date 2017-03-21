using System;
using Model = NTwitch.Rest.API.BlockedUser;

namespace NTwitch.Rest
{
    public class RestBlockedUser : RestEntity<ulong>
    {
        public DateTime UpdatedAt { get; set; }
        public RestUser User { get; set; }

        internal RestBlockedUser(BaseRestClient client, ulong id) 
            : base(client, id) { }

        internal static RestBlockedUser Create(BaseRestClient client, Model model)
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
