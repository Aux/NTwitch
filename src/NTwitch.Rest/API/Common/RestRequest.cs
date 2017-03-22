using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace NTwitch.Rest
{
    public class RestRequest
    {
        public string Method { get; }
        public string Endpoint { get; }
        public Dictionary<string, object> Parameters { get; } = new Dictionary<string, object>();
        public string JsonBody { get; set; }

        public RestRequest(string method, string endpoint)
        {
            Method = method;
            Endpoint = endpoint;
        }

        public HttpRequestMessage GetRequest()
        {
            var endpoint = Endpoint + GetParameterString();
            var request = new HttpRequestMessage(new HttpMethod(Method), endpoint);

            if (!string.IsNullOrWhiteSpace(JsonBody))
            {
                string content = JsonConvert.SerializeObject(JsonBody);
                request.Content = new StringContent(content);
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }
            
            return request;
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
