using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public class RestPostComment : IEntity, IPostComment
    {
        public TwitchRestClient Client { get; }
        [JsonProperty("")]
        public ulong Id { get; internal set; }
        [JsonProperty("")]
        public string Body { get; internal set; }
        [JsonProperty("")]
        public DateTime CreatedAt { get; internal set; }
        [JsonProperty("")]
        public IEnumerable<IEmote> Emotes { get; internal set; }
        [JsonProperty("")]
        public bool IsDeleted { get; internal set; }
        [JsonProperty("")]
        public IPostPermissions Permissions { get; internal set; }
        [JsonProperty("")]
        public IEnumerable<IPostReaction> Reactions { get; internal set; }
        [JsonProperty("")]
        public IUser User { get; internal set; }

        internal RestPostComment(TwitchRestClient client)
        {
            Client = client;
        }

        public async Task CreateReactionAsync(ulong emoteid)
            => await PostHelper.CreateReactionAsync(this, emoteid);

        public async Task DeleteReactionAsync(ulong emoteid)
            => await PostHelper.DeleteReactionAsync(this, emoteid);

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
