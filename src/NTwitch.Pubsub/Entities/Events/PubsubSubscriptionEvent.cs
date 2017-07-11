using System.Collections.Generic;
using System.Linq;
using Model = NTwitch.Pubsub.API.SubscriptionEvent;

namespace NTwitch.Pubsub
{
    public class PubsubSubscriptionEvent : PubsubEvent
    {
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

        internal PubsubSubscriptionEvent(TwitchPubsubClient client)
            : base(client) { }
        
        internal static PubsubSubscriptionEvent Create(TwitchPubsubClient client, Model model)
        {
            var entity = new PubsubSubscriptionEvent(client);
            entity.Update(model);
            return entity;
        }

        internal virtual void Update(Model model)
        {
            base.Update(model);
            Plan = model.SubPlan;
            PlanText = model.SubPlanName;
            Months = model.Months;
            IsResub = model.Context == "resub";

            Message = model.Message.Content;
            Emotes = model.Message.Emotes.Select(x => PubsubEmote.Create(Client, x)).ToArray();
        }
    }
}
