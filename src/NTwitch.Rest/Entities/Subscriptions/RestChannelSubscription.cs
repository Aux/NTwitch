using Model = NTwitch.Rest.API.Subscription;

namespace NTwitch.Rest
{
    public class RestChannelSubscription : RestSubscription
    {
        /// <summary> The channel associated with this subscription </summary>
        public RestChannel Channel { get; private set; }

        internal RestChannelSubscription(BaseRestClient client, string id) 
            : base(client, id) { }

        internal new static RestChannelSubscription Create(BaseRestClient client, Model model)
        {
            var entity = new RestChannelSubscription(client, model.Id);
            entity.Update(model);
            return entity;
        }

        internal override void Update(Model model)
        {
            Channel = new RestChannel(Client, model.Channel.Id);
            Channel.Update(model.Channel);
            base.Update(model);
        }
    }
}
