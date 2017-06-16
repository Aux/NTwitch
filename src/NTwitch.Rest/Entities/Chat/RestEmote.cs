using Model = NTwitch.Rest.API.Emote;

namespace NTwitch.Rest
{
    public class RestEmote : RestEntity<ulong>, IEmote
    {
        /// <summary>  </summary>
        public string Code { get; private set; }
        
        internal RestEmote(BaseTwitchClient client, uint id) 
            : base(client, id) { }

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

        public bool Equals(IEmote x, IEmote y)
            => x.Id == y.Id;
        public int GetHashCode(IEmote obj)
            => obj.GetHashCode();
    }
}
