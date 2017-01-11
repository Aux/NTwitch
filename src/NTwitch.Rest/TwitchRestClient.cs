using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public partial class TwitchRestClient : BaseRestClient, ITwitchClient
    {
        public RestClient Client => ApiClient;

        private string _host;
        
        public TwitchRestClient() : this(new TwitchRestConfig()) { }
        public TwitchRestClient(TwitchRestConfig config)
        {
            _host = config.RestUrl;
        }

        public Task LoginAsync(string clientid)
            => LoginInternalAsync(clientid);
    }
}
