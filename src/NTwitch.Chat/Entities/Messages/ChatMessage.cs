using System;
using Model = NTwitch.Chat.API.MessageReceivedEvent;

namespace NTwitch.Chat
{
    public class ChatMessage : ChatEntity<string>, IMessage
    {
        /// <summary>  </summary>
        public DateTime CreatedAt { get; private set; }
        /// <summary>  </summary>
        public DateTime TmiCreatedAt { get; private set; }
        /// <summary>  </summary>
        public string Badges { get; private set; }
        /// <summary>  </summary>
        public string Emotes { get; private set; }
        /// <summary>  </summary>
        public string Content { get; private set; }
        /// <summary>  </summary>
        public ChatSimpleChannel Channel { get; private set; }
        /// <summary>  </summary>
        public ChatUser User { get; private set; }

        internal ChatMessage(TwitchChatClient client, string id)
            : base(client, id) { }

        public bool Equals(IMessage other)
            => Id == other.Id;

        internal static ChatMessage Create(TwitchChatClient client, Model model)
        {
            var entity = new ChatMessage(client, model.Id);
            entity.Update(model);
            return entity;
        }

        internal virtual void Update(Model model)
        {
            CreatedAt = model.SentTimestamp;
            TmiCreatedAt = model.TmiSentTimestamp;
            Badges = model.Badges;
            Emotes = model.Emotes;
            Content = model.Content;

            Channel = ChatSimpleChannel.Create(Client, model);
            User = ChatUser.Create(Client, model);
        }

    }
}
