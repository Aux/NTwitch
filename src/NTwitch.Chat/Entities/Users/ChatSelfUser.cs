using UserStateModel = NTwitch.Chat.API.UserStateEvent;

namespace NTwitch.Chat
{
    public class ChatSelfUser : ChatUser, ISelfUser
    {
        /// <summary>  </summary>
        public string EmoteSets { get; private set; }
        
        public ChatSelfUser(TwitchChatClient client, ulong id, string name)
            : base(client, id, name) { }

        internal new static ChatSelfUser Create(TwitchChatClient client, UserStateModel model)
        {
            var entity = new ChatSelfUser(client, client.TokenInfo.UserId, model.DisplayName);
            entity.Update(model);
            return entity;
        }

        internal override void Update(UserStateModel model)
        {
            base.Update(model);
            EmoteSets = model.EmoteSets;
        }

        // ISelfUser
        string ISelfUser.Email => null;
    }
}
