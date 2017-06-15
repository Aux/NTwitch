using MsgEventModel = NTwitch.Chat.API.MessageReceivedEvent;

namespace NTwitch.Chat
{
    public class ChatSimpleUser : ChatEntity<ulong>, ISimpleUser
    {
        public string DisplayName { get; private set; }

        internal ChatSimpleUser(TwitchChatClient client, ulong id)
            : base(client, id) { }

        public bool Equals(ISimpleUser other)
            => Id == other.Id;

        internal static ChatSimpleUser Create(TwitchChatClient client, MsgEventModel model)
        {
            var entity = new ChatSimpleUser(client, model.UserId);
            entity.Update(model);
            return entity;
        }

        internal virtual void Update(MsgEventModel model)
        {
            DisplayName = model.DisplayName;
        }

        // IUser
        public string AvatarUrl => null;
        public string Name => null;
    }
}
