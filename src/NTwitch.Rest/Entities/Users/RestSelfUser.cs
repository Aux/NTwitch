using System;
using System.Threading.Tasks;
using Model = NTwitch.Rest.API.User;

namespace NTwitch.Rest
{
    public class RestSelfUser : RestUser, ISelfUser
    {
        /// <summary> The email associated with this user account </summary>
        public string Email { get; private set; }
        /// <summary> True if this user's email is verified </summary>
        public bool IsVerified { get; private set; }
        /// <summary> True if this user is a partner </summary>
        public bool IsPartnered { get; private set; }
        /// <summary> True if this user has connected their twitter account </summary>
        public bool IsTwitterConnected { get; private set; }
        /// <summary> This user's notification settings </summary>
        public RestUserNotifications Notifications { get; private set; }
        
        public RestSelfUser(BaseTwitchClient client, ulong id, string name) 
            : base(client, id, name) { }

        internal new static RestSelfUser Create(BaseTwitchClient client, Model model)
        {
            var entity = new RestSelfUser(client, model.Id, model.Name);
            entity.Update(model);
            return entity;
        }

        internal override void Update(Model model)
        {
            base.Update(model);
            Email = model.Email;
            IsVerified = model.IsVerified;
            IsPartnered = model.IsPartnered;
            IsTwitterConnected = model.IsTwitterConnected;
            Notifications = RestUserNotifications.Create(model.Notifications);
        }

        /// <summary> Get the most recent version of this entity </summary>
        public override async Task UpdateAsync()
        {
            var model = await Client.ApiClient.GetMyUserAsync(null).ConfigureAwait(false);
            Update(model);
        }
    }
}
