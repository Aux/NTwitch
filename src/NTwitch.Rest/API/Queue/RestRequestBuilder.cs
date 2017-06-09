using System.Collections.Generic;

namespace NTwitch.Rest.Queue
{
    public class RestRequestBuilder
    {
        public string Method => _defaultMethod;
        public string Endpoint => _defaultEndpoint + GetParameterString();
        public Dictionary<string, object> Parameters { get; protected set; }

        internal string _defaultMethod;
        internal string _defaultEndpoint;

        public RestRequestBuilder(string method, string endpoint)
        {
            _defaultMethod = method;
            _defaultEndpoint = endpoint;
            Parameters = new Dictionary<string, object>();
        }
        
        public string GetParameterString()
        {
            if (Parameters.Count == 0)
                return "";

            List<string> paramList = new List<string>();
            foreach (var p in Parameters)
                if (p.Value != null) paramList.Add($"{p.Key}={p.Value}");
            return $"?{string.Join("&", paramList.ToArray())}";
        }
    }
}
