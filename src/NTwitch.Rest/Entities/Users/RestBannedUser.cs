using System;
using Model = NTwitch.Rest.API.User;

namespace NTwitch.Rest
{
    public class RestBannedUser : RestSimpleUser
    {
        public DateTime StartAt { get; private set; }
        public DateTime EndAt { get; private set; }
        public string Bio { get; private set; }

        internal RestBannedUser(BaseRestClient client, ulong id)
            : base(client, id) { }
        
        internal new static RestBannedUser Create(BaseRestClient client, Model model)
        {
            var entity = new RestBannedUser(client, model.Id);
            entity.Update(model);
            return entity;
        }

        internal override void Update(Model model)
        {
            base.Update(model);
            StartAt = model.StartAt;
            EndAt = model.EndAt;
            Bio = model.Bio;
        }
    }
}
