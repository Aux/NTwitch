using System;
using System.Collections.Generic;
using System.Linq;
using Model = NTwitch.Pubsub.API.Subscription;

namespace NTwitch.Pubsub
{
    public class PubsubSubscription
    {
        /// <summary> An instance of the client that created this entity </summary>
        public TwitchPubsubClient Client { get; }
        /// <summary> The channel being subscribed to </summary>
        public PubsubSimpleChannel Channel { get; private set; }
        /// <summary> The user who purchased the subscription </summary>
        public PubsubSimpleUser User { get; private set; }
        /// <summary> The date and time this subscription occurred </summary>
        public DateTime Timestamp { get; private set; }
        /// <summary> The type of subscription </summary>
        public string Plan { get; private set; }
        /// <summary> The text associated with this sub plan </summary>
        public string PlanText { get; private set; }
        /// <summary> The number of months this user has subscribed </summary>
        public int Months { get; private set; }
        /// <summary> True if this is not this user's first subscription </summary>
        public bool IsResub { get; private set; }
        /// <summary> The message associated with this subscription </summary>
        public string Message { get; private set; }
        /// <summary> A collection of emotes found in the sub message </summary>
        public IReadOnlyCollection<PubsubEmote> Emotes { get; private set; }

        internal PubsubSubscription(TwitchPubsubClient client)
        {
            Client = client;
        }
        
        internal static PubsubSubscription Create(TwitchPubsubClient client, Model model)
        {
            var entity = new PubsubSubscription(client);
            entity.Update(model);
            return entity;
        }

        internal virtual void Update(Model model)
        {
            Channel = PubsubSimpleChannel.Create(Client, model);
            User = PubsubSimpleUser.Create(Client, model);

            Timestamp = model.Timestamp;
            Plan = model.SubPlan;
            PlanText = model.SubPlanName;
            Months = model.Months;
            IsResub = model.Context == "resub";

            Message = model.Message.Content;
            Emotes = model.Message.Emotes.Select(x => PubsubEmote.Create(Client, x)).ToArray();
        }
    }
}
