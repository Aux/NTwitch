using System;
using System.Threading.Tasks;
using MsgEventModel = NTwitch.Chat.API.MessageReceivedEvent;
using UserStateModel = NTwitch.Chat.API.UserStateEvent;
using NoticeModel = NTwitch.Chat.API.UserNoticeEvent;

namespace NTwitch.Chat
{
    public class ChatUser : ChatSimpleUser, IUser
    {
        /// <summary>  </summary>
        public string Color { get; private set; }
        /// <summary>  </summary>
        public UserType Type { get; private set; }
        /// <summary>  </summary>
        public bool IsMod { get; private set; }
        /// <summary>  </summary>
        public bool IsSubscriber { get; private set; }
        /// <summary>  </summary>
        public bool IsTurbo { get; private set; }
        
        internal ChatUser(TwitchChatClient client, ulong id) 
            : base(client, id) { }

        internal static UserType GetUserType(string tag)
        {
            switch (tag)
            {
                case "mod":
                    return UserType.Moderator;
                case "global_mod":
                    return UserType.GlobalModerator;
                case "admin":
                    return UserType.Admin;
                case "staff":
                    return UserType.Staff;
                default:
                    return UserType.None;
            }
        }

        internal new static ChatUser Create(TwitchChatClient client, MsgEventModel model)
        {
            var entity = new ChatUser(client, model.UserId);
            entity.Update(model);
            return entity;
        }

        internal new static ChatUser Create(TwitchChatClient client, UserStateModel model)
        {
            var entity = new ChatUser(client, client.TokenInfo.UserId);
            entity.Update(model);
            return entity;
        }

        internal new static ChatUser Create(TwitchChatClient client, NoticeModel model)
        {
            var entity = new ChatUser(client, model.UserId);
            entity.Update(model);
            return entity;
        }

        internal override void Update(MsgEventModel model)
        {
            base.Update(model);
            Color = model.Color;
            Type = GetUserType(model.UserType);
            IsMod = model.IsMod;
            IsSubscriber = model.IsSubscriber;
            IsTurbo = model.IsTurbo;
        }

        internal override void Update(UserStateModel model)
        {
            base.Update(model);
            Color = model.Color;
            Type = GetUserType(model.UserType);
            IsMod = model.IsMod;
            IsSubscriber = model.IsSubscriber;
            IsTurbo = false;                        // Turbo is found in badges for this model
        }

        internal override void Update(NoticeModel model)
        {
            base.Update(model);
            Color = model.Color;
            Type = GetUserType(model.UserType);
            IsMod = model.IsMod;
            IsTurbo = model.IsTurbo;
            IsSubscriber = model.IsSubscriber;
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
