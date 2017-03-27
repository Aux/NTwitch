using Model = NTwitch.Rest.API.Emote;

namespace NTwitch.Rest
{
    public class RestEmote : RestEntity<uint>
    {
        /// <summary>  </summary>
        public string Code { get; private set; }
        
        internal RestEmote(BaseRestClient client, uint id) 
            : base(client, id) { }

        internal static RestEmote Create(BaseRestClient client, Model model)
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
