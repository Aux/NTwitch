using System;
using Model = NTwitch.Rest.API.Subscription;

namespace NTwitch.Rest
{
    public class RestSubscription : RestEntity<string>
    {
        /// <summary> The date and time this subscription was created </summary>
        public DateTime CreatedAt { get; private set; }
        /// <summary> The number that represents this subscription plan </summary>
        public int Plan { get; private set; }
        /// <summary> The name of the subscription plan </summary>
        public string PlanName { get; private set; }

        internal RestSubscription(BaseRestClient client, string id) 
            : base(client, id) { }

        internal static RestSubscription Create(BaseRestClient client, Model model)
        {
            var entity = new RestSubscription(client, model.Id);
            entity.Update(model);
            return entity;
        }

        internal virtual void Update(Model model)
        {
            CreatedAt = model.CreatedAt;
            Plan = model.SubPlan;
            PlanName = model.SubPlanName;
        }
    }
}
