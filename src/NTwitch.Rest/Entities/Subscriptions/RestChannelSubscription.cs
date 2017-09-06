using Model = NTwitch.Rest.API.Subscription;

namespace NTwitch.Rest
{
    public class RestChannelSubscription : RestSubscription
    {
        /// <summary> The channel associated with this subscription </summary>
        public RestChannel Channel { get; private set; }

        internal RestChannelSubscription(BaseTwitchClient client, string id) 
            : base(client, id) { }

        internal new static RestChannelSubscription Create(BaseTwitchClient client, Model model)
        {
            var entity = new RestChannelSubscription(client, model.Id);
            entity.Update(model);
            return entity;
        }

        internal override void Update(Model model)
        {
            base.Update(model);
            Channel = RestChannel.Create(Client, model.Channel);
        }
    }
}
