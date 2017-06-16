using System.Collections.Generic;
using System.IO;
using System.Net;

namespace NTwitch.Rest
{
    public class RestResponse
    {
        public HttpStatusCode StatusCode { get; }
        public Dictionary<string, string> Headers { get; }
        public Stream Body { get; }

        public RestResponse(HttpStatusCode code, Dictionary<string, string> headers, Stream body)
        {
            StatusCode = code;
            Headers = headers;
            Body = body;
        }
    }
}
