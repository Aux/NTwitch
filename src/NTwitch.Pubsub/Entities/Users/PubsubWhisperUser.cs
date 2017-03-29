using System.Collections.Generic;
using System.Linq;
using UserModel = NTwitch.Pubsub.API.WhisperMessage;
using SelfModel = NTwitch.Pubsub.API.WhisperUser;

namespace NTwitch.Pubsub
{
    public class PubsubWhisperUser : PubsubSimpleUser
    {
        public string DisplayName { get; private set; }
        public string Color { get; private set; }
        //public IReadOnlyCollection<object> Badges { get; private set; }
        //public IReadOnlyCollection<object> Emotes { get; private set; }

        public PubsubWhisperUser(BasePubsubClient client, ulong id)
            : base(client, id) { }
        
        internal new static PubsubWhisperUser Create(BasePubsubClient client, UserModel model)
        {
            var entity = new PubsubWhisperUser(client, model.FromId);
            entity.Update(model);
            return entity;
        }

        internal new static PubsubWhisperUser Create(BasePubsubClient client, SelfModel model)
        {
            var entity = new PubsubWhisperUser(client, model.Id);
            entity.Update(model);
            return entity;
        }

        internal override void Update(UserModel model)
        {
            base.Update(model);
            DisplayName = model.Tags.DisplayName;
            Color = model.Tags.Color;
            //Badges = model.Tags.Badges.ToArray();
            //Emotes = model.Tags.Emotes.ToArray();
        }

        internal override void Update(SelfModel model)
        {
            base.Update(model);
            DisplayName = model.DisplayName;
            Color = model.Color;
            //Badges = model.Badges.ToArray();
            //Emotes = new List<object>().ToArray();
        }
    }
}
