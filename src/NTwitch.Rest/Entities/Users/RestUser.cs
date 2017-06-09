using System;
using System.Threading.Tasks;
using Model = NTwitch.Rest.API.User;

namespace NTwitch.Rest
{
    public class RestUser : RestSimpleUser
    {
        /// <summary> The date and time this user was created </summary>
        public DateTime CreatedAt { get; private set; }
        /// <summary> The date and time this user was last updated </summary>
        public DateTime UpdatedAt { get; private set; }
        /// <summary>  </summary>
        public string Type { get; private set; }
        /// <summary> This user's profile description </summary>
        public string Bio { get; private set; }
        
        internal RestUser(BaseTwitchClient client, ulong id) 
            : base(client, id) { }

        internal new static RestUser Create(BaseTwitchClient client, Model model)
        {
            var entity = new RestUser(client, model.Id);
            entity.Update(model);
            return entity;
        }

        internal override void Update(Model model)
        {
            CreatedAt = model.CreatedAt;
            UpdatedAt = model.UpdatedAt;
            Type = model.Type;
            Bio = model.Bio;
            base.Update(model);
        }
    }
}
