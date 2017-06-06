using System;
using Model = NTwitch.Rest.API.User;

namespace NTwitch.Rest
{
    public class RestBannedUser : RestSimpleUser
    {
        /// <summary> The date and time that this user was banned </summary>
        public DateTime StartAt { get; private set; }
        /// <summary> The date and time that this ban will expire </summary>
        public DateTime EndAt { get; private set; }
        /// <summary> The description provided for this user </summary>
        public string Bio { get; private set; }

        internal RestBannedUser(TwitchRestClient client, ulong id)
            : base(client, id) { }
        
        internal new static RestBannedUser Create(TwitchRestClient client, Model model)
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
