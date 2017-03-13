using Newtonsoft.Json;
using System.Net;

namespace NTwitch.Rest
{
    public class RestResponse
    {
        public HttpStatusCode StatusCode { get; }
        public string Body { get; }

        public RestResponse(HttpStatusCode code, string body)
        {
            StatusCode = code;
            Body = body;
        }

        public T GetBodyAsType<T>()
            => JsonConvert.DeserializeObject<T>(Body);
    }
}
