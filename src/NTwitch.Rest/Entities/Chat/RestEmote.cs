using System;
using Model = NTwitch.Rest.API.Emote;

namespace NTwitch.Rest
{
    public class RestEmote : RestEntity<ulong>, IEquatable<RestEmote>
    {
        /// <summary>  </summary>
        public string Code { get; private set; }
        
        internal RestEmote(BaseTwitchClient client, uint id) 
            : base(client, id) { }

        public bool Equals(RestEmote other)
            => Id == other.Id;
        public override string ToString()
            => Code;

        internal static RestEmote Create(BaseTwitchClient client, Model model)
        {
            var entity = new RestEmote(client, model.Id);
            entity.Update(model);
            return entity;
        }

        internal virtual void Update(Model model)
        {
            Code = model.Code;
        }
    }
}
