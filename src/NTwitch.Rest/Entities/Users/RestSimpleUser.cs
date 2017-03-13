using System;
using System.Threading.Tasks;
using Model = NTwitch.Rest.API.SimpleUser;

namespace NTwitch.Rest
{
    public class RestSimpleUser : RestEntity<ulong>, IUser
    {
        public string LogoUrl { get; private set; }
        public string Name { get; private set; }
        public string DisplayName { get; private set; }

        internal RestSimpleUser(BaseRestClient client, ulong id) 
            : base(client, id) { }

        internal static RestSimpleUser Create(BaseRestClient client, Model model)
        {
            var entity = new RestSimpleUser(client, model.Id);
            entity.Update(model);
            return entity;
        }

        internal virtual void Update(Model model)
        {
            DisplayName = model.DisplayName;
            Name = model.Name;
        }

        public virtual Task UpdateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
