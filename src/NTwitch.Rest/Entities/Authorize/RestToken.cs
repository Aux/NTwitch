using System;
using System.Threading.Tasks;
using Model = NTwitch.Rest.API.Token;

namespace NTwitch.Rest
{
    public class RestToken
    {
        public bool IsValid { get; private set; }
        public string Username { get; private set; }
        public ulong? UserId { get; private set; }
        public string ClientId { get; private set; }
        public RestAuthorization Authorization { get; private set; } = new RestAuthorization();

        internal RestToken() { }

        internal static RestToken Create(Model model)
        {
            var entity = new RestToken();
            entity.Update(model);
            return entity;
        }

        internal virtual void Update(Model model)
        {
            IsValid = model.IsValid;
            Username = model?.Username;
            UserId = model?.UserId;
            ClientId = model?.ClientId;
            if (model.Authorization != null)
                Authorization.Update(model.Authorization);
        }

        public virtual Task UpdateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
