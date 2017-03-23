using System;
using System.Collections.Generic;
using System.Linq;
using Model = NTwitch.Rest.API.Authorization;

namespace NTwitch.Rest
{
    public class RestAuthorization
    {
        public IReadOnlyCollection<string> Scopes { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        internal RestAuthorization() { }

        internal static RestAuthorization Create(Model model)
        {
            var entity = new RestAuthorization();
            entity.Update(model);
            return entity;
        }

        internal virtual void Update(Model model)
        {
            Scopes = model.Scopes.ToArray();
            CreatedAt = model.CreatedAt;
            UpdatedAt = model.UpdatedAt;
        }
    }
}
