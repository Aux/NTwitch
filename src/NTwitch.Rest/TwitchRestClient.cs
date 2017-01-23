using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public partial class TwitchRestClient : BaseRestClient, ITwitchClient
    {
        public RestClient Client => ApiClient;
        
        public TwitchRestClient() : this(new TwitchRestConfig()) { }
        public TwitchRestClient(TwitchRestConfig config) : base(config) { }

        public Task LoginAsync(TokenType type, string token)
            => LoginInternalAsync(type, token);
    }
}
