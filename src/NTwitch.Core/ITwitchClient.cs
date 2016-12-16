using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch
{
    public interface ITwitchClient
    {
        ConnectionState ConnectionState { get; }

        Task LoginAsync(string clientid, string token = null);
    }
}
