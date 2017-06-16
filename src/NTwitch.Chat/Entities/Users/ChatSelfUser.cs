using UserStateModel = NTwitch.Chat.API.UserStateEvent;

namespace NTwitch.Chat
{
    public class ChatSelfUser : ChatUser, ISelfUser
    {
        /// <summary>  </summary>
        public string EmoteSets { get; private set; }
        
        public ChatSelfUser(TwitchChatClient client, ulong id)
            : base(client, id) { }

        internal new static ChatUser Create(TwitchChatClient client, UserStateModel model)
        {
            var entity = new ChatUser(client, client.TokenInfo.UserId);
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
