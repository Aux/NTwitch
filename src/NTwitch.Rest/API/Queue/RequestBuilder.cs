using System.Collections.Generic;

namespace NTwitch.Rest.Queue
{
    public class RequestBuilder
    {
        public string Method => _method;
        public string Endpoint => _defaultEndpoint + GetParameterString();

        private string _method;
        private string _defaultEndpoint;
        private Dictionary<string, object> _endpointParams;

        public RequestBuilder(string method, string endpoint)
        {
            _method = method;
            _defaultEndpoint = endpoint;
            _endpointParams = new Dictionary<string, object>();
        }

        public void SetDefaultEndpoint(string endpoint)
        {
            _defaultEndpoint = endpoint;
        }

        public void SetParameter(string key, object value = null)
        {
            _endpointParams.Remove(key);
            if (value != null)
                _endpointParams.Add(key, value);
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
