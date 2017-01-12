using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public partial class TwitchRestClient : BaseRestClient, ITwitchClient
    {
        public RestClient Client => ApiClient;
        
        public TwitchRestClient() : this(new TwitchRestConfig()) { }
        public TwitchRestClient(TwitchRestConfig config) : base(config) { }

        public Task LoginAsync(string clientid, string token = null)
            => LoginInternalAsync(clientid, token);
    }
}
