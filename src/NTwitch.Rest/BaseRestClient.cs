using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public class BaseRestClient : ITwitchClient
    {
        internal Task LoginInternalAsync(string clientid)
        {
            throw new NotImplementedException();
        }



        public virtual Task ConnectAsync()
            => throw new NotSupportedException();
        public virtual Task DisconnectAsync()
            => throw new NotSupportedException();
        Task ITwitchClient.LoginAsync()
            => Task.CompletedTask;
    }
}
