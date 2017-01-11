using System;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public class BaseRestClient : ITwitchClient
    {
        internal RestClient ApiClient => _rest;

        private RestClient _rest;
        
        internal async Task LoginInternalAsync(string clientid)
        {
            _rest = new RestClient();
            await _rest.LoginAsync(clientid);
        }

        Task ITwitchClient.ConnectAsync()
            => Task.CompletedTask;
        Task ITwitchClient.DisconnectAsync()
            => Task.CompletedTask;
        Task ITwitchClient.LoginAsync()
            => Task.CompletedTask;
    }
}
