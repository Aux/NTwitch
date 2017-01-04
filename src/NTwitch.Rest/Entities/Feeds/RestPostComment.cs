using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;


namespace NTwitch.Rest
{
    public class RestPostComment : IEntity, IPostComment
    {
        public TwitchRestClient Client { get; }
        public ulong Id { get; internal set; }
        public string Body { get; internal set; }
        public DateTime CreatedAt { get; internal set; }
        public IEnumerable<IEmote> Emotes { get; internal set; }
        public bool IsDeleted { get; internal set; }
        public IPostPermissions Permissions { get; internal set; }
        public IEnumerable<IPostReaction> Reactions { get; internal set; }
        public IUser User { get; internal set; }

        internal RestPostComment(ITwitchClient client)
        {
            Client = client as TwitchRestClient;
        }

        public Task CreateReactionAsync(ulong emoteid)
        {
            throw new NotImplementedException();
        }

        public Task DeleteReactionAsync(ulong emoteid)
        {
            throw new NotImplementedException();
        }

        //IPostComment
        ITwitchClient IEntity.Client
            => Client;
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
