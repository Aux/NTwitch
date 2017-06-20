using RoomStateModel = NTwitch.Chat.API.RoomStateEvent;

namespace NTwitch.Chat
{
    public class ChatChannel : ChatSimpleChannel//, IChannel
    {
        public string BroadcasterLanguage { get; private set; }
        public int FollowersOnlyMode { get; private set; }
        public bool IsFollowersOnly { get; private set; }
        public bool IsR9k { get; private set; }
        public bool IsSlow { get; private set; }
        public bool IsSubsOnly { get; private set; }

        public ChatChannel(TwitchChatClient client, ulong id)
            : base(client, id) { }

        internal new static ChatChannel Create(TwitchChatClient client, RoomStateModel model)
        {
            var entity = new ChatChannel(client, model.ChannelId);
            entity.Update(model);
            return entity;
        }

        internal override void Update(RoomStateModel model)
        {
            base.Update(model);
            BroadcasterLanguage = model.BroadcasterLang;
            FollowersOnlyMode = model.FollowersOnlyMode;
            IsFollowersOnly = model.FollowersOnlyMode > 0;
            IsR9k = model.IsR9k;
            IsSlow = model.IsSlow;
            IsSubsOnly = model.IsSubsOnly;
        }
    }
}
