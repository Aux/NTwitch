using Model = NTwitch.Chat.API.UserNoticeEvent;

namespace NTwitch.Chat
{
    public class ChatNoticeMessage : ChatMessage
    {
        /// <summary>  </summary>
        public int Months { get; private set; }
        /// <summary>  </summary>
        public string Title { get; private set; }
        /// <summary>  </summary>
        public string Plan { get; private set; }
        /// <summary>  </summary>
        public string SystemMessage { get; private set; }
        /// <summary>  </summary>
        public string Type { get; private set; }

        internal ChatNoticeMessage(TwitchChatClient client, string id)
            : base(client, id) { }

        internal new static ChatNoticeMessage Create(TwitchChatClient client, Model model)
        {
            var entity = new ChatNoticeMessage(client, model.Id);
            entity.Update(model);
            return entity;
        }

        internal override void Update(Model model)
        {
            base.Update(model);

            Months = model.Months;
            Title = model.PlanName;
            Plan = model.Plan;
            SystemMessage = model.SystemMessage;
            Type = model.Type;
        }
    }
}
