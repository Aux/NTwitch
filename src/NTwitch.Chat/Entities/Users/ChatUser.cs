using System;
using System.Threading.Tasks;
using MsgEventModel = NTwitch.Chat.API.MessageReceivedEvent;

namespace NTwitch.Chat
{
    public class ChatUser : ChatSimpleUser, IUser
    {
        /// <summary>  </summary>
        public string Color { get; private set; }
        /// <summary>  </summary>
        public string UserType { get; private set; }
        /// <summary>  </summary>
        public bool IsMod { get; private set; }
        /// <summary>  </summary>
        public bool IsSubscriber { get; private set; }
        /// <summary>  </summary>
        public bool IsTurbo { get; private set; }
        
        internal ChatUser(TwitchChatClient client, ulong id) 
            : base(client, id) { }
        
        internal new static ChatUser Create(TwitchChatClient client, MsgEventModel model)
        {
            var entity = new ChatUser(client, model.UserId);
            entity.Update(model);
            return entity;
        }

        internal override void Update(MsgEventModel model)
        {
            base.Update(model);
            Color = model.Color;
            UserType = model.UserType;
            IsMod = model.IsMod;
            IsSubscriber = model.IsSubscriber;
            IsTurbo = model.IsTurbo;
        }

        /// <summary>  </summary>
        public Task UpdateAsync()
        {
            throw new NotImplementedException();
        }

        // IUser
        string ISimpleUser.AvatarUrl => null;
    }
}
