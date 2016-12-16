using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch
{
    public interface ISubscription
    {
        DateTime CreatedAt { get; }
        string Id { get; }
        string[] Links { get; }
    }
}
