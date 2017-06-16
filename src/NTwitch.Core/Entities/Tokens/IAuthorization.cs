using System;
using System.Collections.Generic;

namespace NTwitch
{
    public interface IAuthorization
    {
        IReadOnlyCollection<string> Scopes { get; }
        DateTime CreatedAt { get; }
        DateTime UpdatedAt { get; }
    }
}
