using Model = NTwitch.Rest.API.Subscription;

namespace NTwitch.Rest
{
    public class RestUserSubscription : RestSubscription
    {
        /// <summary> The user associated with this subscription </summary>
        public RestUser User { get; private set; }

        internal RestUserSubscription(BaseTwitchClient client, string id) 
            : base(client, id) { }

        internal new static RestUserSubscription Create(BaseTwitchClient client, Model model)
        {
            var entity = new RestUserSubscription(client, model.Id);
            entity.Update(model);
            return entity;
        }

        internal override void Update(Model model)
        {
            base.Update(model);
            User = RestUser.Create(Client, model.User);
        }
    }
}
