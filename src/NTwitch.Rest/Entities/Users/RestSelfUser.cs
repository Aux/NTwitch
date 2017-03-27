using System.Threading.Tasks;
using Model = NTwitch.Rest.API.User;

namespace NTwitch.Rest
{
    public class RestSelfUser : RestUser
    {
        /// <summary> The email associated with this user account </summary>
        public string Email { get; private set; }
        /// <summary> True if this user's email is verified </summary>
        public bool IsVerified { get; private set; }
        /// <summary> True if this user is a partner </summary>
        public bool IsPartner { get; private set; }
        /// <summary> True if this user has connected their twitter account </summary>
        public bool IsTwitterConnected { get; private set; }
        /// <summary> This user's notification settings </summary>
        public RestUserNotifications Notifications { get; private set; }
        
        public RestSelfUser(BaseRestClient client, ulong id) 
            : base(client, id) { }

        internal new static RestSelfUser Create(BaseRestClient client, Model model)
        {
            var entity = new RestSelfUser(client, model.Id);
            entity.Update(model);
            return entity;
        }

        internal override void Update(Model model)
        {
            base.Update(model);
            Email = model.Email;
            IsVerified = model.IsVerified;
            IsPartner = model.IsPartner;
            IsTwitterConnected = model.IsTwitterConnected;
            Notifications.Update(model.Notifications);
        }

        /// <summary> Update this user's properties </summary>
        public override async Task UpdateAsync()
        {
            var entity = await Client.RestClient.GetCurrentUserAsync().ConfigureAwait(false);
            Update(entity);
        }
    }
}
