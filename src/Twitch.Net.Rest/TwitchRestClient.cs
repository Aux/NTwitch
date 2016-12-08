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

        /// <summary> Authenticate this client with the twitch oauth servers. </summary>
        public async Task LoginAsync(string token)
            => await _rest.LoginAsync(token);

        /// <summary> Get information about a user. </summary>
        public async Task<RestUser> GetUserAsync(string name)
            => await _rest.SendAsync<RestUser>("GET", "users/" + name);

        /// <summary> Get information about the current user. </summary>
        public async Task<RestSelfUser> GetCurrentUserAsync()
            => await _rest.SendAsync<RestSelfUser>("GET", "user");

        /// <summary> Get the top streamed games on Twitch. </summary>
        public async Task GetTopGamesAsync()
            => await _rest.SendAsync<RestTopGame>("GET", "games/top");

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
