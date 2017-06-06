using System.Collections.Generic;

namespace NTwitch.Rest.Queue
{
    public class RequestBuilder
    {
        public string Method { get; }
        public string Endpoint => _defaultEndpoint + GetParameterString();
        
        internal string _defaultEndpoint;
        internal Dictionary<string, object> _endpointParams;

        public RequestBuilder(string method, string endpoint)
        {
            Method = method;
            _defaultEndpoint = endpoint;
            _endpointParams = new Dictionary<string, object>();
        }
        
        public string GetParameterString()
        {
            if (_endpointParams.Count == 0)
                return "";

            List<string> paramList = new List<string>();
            foreach (var p in _endpointParams)
                if (p.Value != null) paramList.Add($"{p.Key}={p.Value}");
            return $"?{string.Join("&", paramList.ToArray())}";
        }
    }
}
