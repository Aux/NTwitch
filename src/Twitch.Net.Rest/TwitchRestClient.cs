using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Twitch.Rest
{
    public class TwitchRestClient : ITwitchClient
    {
        private TwitchRestClientConfig _config;
        private string _token;
        
        public TwitchRestClient() { }
        public TwitchRestClient(TwitchRestClientConfig config)
        {
            _config = config;
        }
        
        // ITwitchClient
        public ConnectionState ConnectionState { get; }

        public Task LoginAsync(string token)
        {
            throw new NotImplementedException();
        }

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
