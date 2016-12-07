using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Twitch.Rest
{
    public class TwitchRestClient : ITwitchClient
    {
        private TwitchRestClientConfig _config;
        private RestApiClient _rest;
        private string _token;
        
        public TwitchRestClient() { }
        public TwitchRestClient(TwitchRestClientConfig config)
        {
            _config = config;
        }

        public Task LoginAsync(string token)
        {
            throw new NotImplementedException();
        }

        // ITwitchClient
        public ConnectionState ConnectionState { get; }

        public Task ConnectAsync()
        {
            throw new NotImplementedException();
        }

        public Task DisconnectAsync()
        {
            throw new NotImplementedException();
        }
    }
}
