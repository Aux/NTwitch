using System;
using MessageModel = NTwitch.Chat.API.MessageReceivedEvent;
using NoticeModel = NTwitch.Chat.API.UserNoticeEvent;

namespace NTwitch.Chat
{
    public class ChatMessage : ChatEntity<string>, IMessage
    {
        /// <summary>  </summary>
        public DateTime CreatedAt { get; private set; }
        /// <summary>  </summary>
        public string Badges { get; private set; }
        /// <summary>  </summary>
        public string Emotes { get; private set; }
        /// <summary>  </summary>
        public string Content { get; private set; }
        /// <summary>  </summary>
        public ChatSimpleChannel Channel { get; private set; }
        /// <summary>  </summary>
        public ChatUser Author { get; private set; }

        internal ChatMessage(TwitchChatClient client, string id)
            : base(client, id) { }

        public bool Equals(IMessage other)
            => Id == other.Id;

        internal static ChatMessage Create(TwitchChatClient client, MessageModel model)
        {
            var entity = new ChatMessage(client, model.Id);
            entity.Update(model);
            return entity;
        }

        internal static ChatMessage Create(TwitchChatClient client, NoticeModel model)
        {
            var entity = new ChatMessage(client, model.Id);
            entity.Update(model);
            return entity;
        }

        internal virtual void Update(MessageModel model)
        {
            CreatedAt = model.SentTimestamp;
            Badges = model.Badges;
            Emotes = model.Emotes;
            Content = model.Content;

            Channel = ChatSimpleChannel.Create(Client, model);
            Author = ChatUser.Create(Client, model);
        }

        internal virtual void Update(NoticeModel model)
        {
            CreatedAt = model.TmiSentTimestamp;
            Badges = model.Badges;
            Emotes = model.Emotes;
            Content = model.Content;

            Channel = ChatSimpleChannel.Create(Client, model);
            Author = ChatUser.Create(Client, model);
        }
    }
}
