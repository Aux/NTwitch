using System;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public class BaseRestClient : ITwitchClient
    {
        internal RestClient ApiClient => _rest;

        private RestClient _rest;

        internal Task LoginInternalAsync(string clientid)
        {
            _rest = new RestClient();

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
