using System.Collections.Generic;
using System.Threading;

namespace NTwitch
{
    public class RequestOptions
    {
        public static RequestOptions Default => new RequestOptions();

        public int? Timeout { get; set; }
        public CancellationToken CancelToken { get; set; } = CancellationToken.None;
        public RetryMode? RetryMode { get; set; }
        public bool HeaderOnly { get; internal set; }

        internal bool IgnoreState { get; set; }
        internal bool AllowUnauthenticated { get; set; }
        internal HashSet<string> RequiredScopes { get; set; } = new HashSet<string>();

        internal void AddRequiredScopes(params string[] scopes)
        {
            foreach (var scope in scopes)
                RequiredScopes.Add(scope);
        }

        internal static RequestOptions CreateOrClone(RequestOptions options)
        {
            if (options == null)
                return new RequestOptions();
            else
                return options.Clone();
        }
        
        public RequestOptions Clone() => MemberwiseClone() as RequestOptions;
    }
}
