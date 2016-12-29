using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public class RestPostComment : RestEntity, IPostComment
    {
        public string Body { get; }
        public DateTime CreatedAt { get; }
        public IEnumerable<IEmote> Emotes { get; }
        public bool IsDeleted { get; }
        public IPostPermissions Permissions { get; }
        public IEnumerable<IPostReaction> Reactions { get; }
        public IUser User { get; }
        
        public RestPostComment(TwitchRestClient client, ulong id) : base(client, id) { }

        public Task CreateReactionAsync(ulong emoteid)
        {
            throw new NotImplementedException();
        }

        public Task DeleteReactionAsync(ulong emoteid)
        {
            throw new NotImplementedException();
        }

        //IPostComment
        IEnumerable<IEmote> IPostComment.Emotes
            => Emotes;
        IPostPermissions IPostComment.Permissions
            => Permissions;
        IEnumerable<IPostReaction> IPostComment.Reactions
            => Reactions;
        IUser IPostComment.User
            => User;
    }
}
