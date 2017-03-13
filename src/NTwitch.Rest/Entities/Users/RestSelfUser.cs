using System;
using System.Threading.Tasks;
using Model = NTwitch.Rest.API.SelfUser;

namespace NTwitch.Rest
{
    public class RestSelfUser : RestUser
    {
        public string Email { get; private set; }
        public bool IsVerified { get; private set; }
        public bool IsPartner { get; private set; }
        public bool IsTwitterConnected { get; private set; }
        public RestUserNotifications Notifications { get; private set; }
        
        public RestSelfUser(BaseRestClient client, ulong id) 
            : base(client, id) { }

        internal static RestSelfUser Create(BaseRestClient client, Model model)
        {
            var entity = new RestSelfUser(client, model.Id);
            entity.Update(model);
            return entity;
        }

        internal virtual void Update(Model model)
        {
            base.Update(model);
            Email = model.Email;
            IsVerified = model.IsVerified;
            IsPartner = model.IsPartner;
            IsTwitterConnected = model.IsTwitterConnected;
            Notifications.Update(model.Notifications);
        }

        public override Task UpdateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
