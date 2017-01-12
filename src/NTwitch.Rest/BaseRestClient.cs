using System;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public class BaseRestClient : ITwitchClient
    {
        internal RestClient ApiClient => _rest;
        internal LogManager Logger => _log;

        private LogManager _log;
        private RestClient _rest;
        private string _resthost;
        
        public BaseRestClient(TwitchRestConfig config)
        {
            _log = new LogManager(config.LogLevel);
        }

        internal Task LoginInternalAsync(string clientid, string token)
        {
            _rest = new RestClient(_log, _resthost, clientid, token);
            return Task.CompletedTask;
        }

        Task ITwitchClient.ConnectAsync()
            => Task.CompletedTask;
        Task ITwitchClient.DisconnectAsync()
            => Task.CompletedTask;
        Task ITwitchClient.LoginAsync()
            => Task.CompletedTask;
    }
}
