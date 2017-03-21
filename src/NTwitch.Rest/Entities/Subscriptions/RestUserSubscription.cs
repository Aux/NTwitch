using Model = NTwitch.Rest.API.UserSubscription;

namespace NTwitch.Rest
{
    public class RestUserSubscription : RestSubscription
    {
        public RestUser User { get; private set; }

        internal RestUserSubscription(BaseRestClient client) : base(client) { }

        internal static RestUserSubscription Create(BaseRestClient client, Model model)
        {
            var entity = new RestUserSubscription(client);
            entity.Update(model);
            return entity;
        }

        internal virtual void Update(Model model)
        {
            User = new RestUser(Client, model.User.Id);
            User.Update(model.User);
            base.Update(model);
        }
    }
}
