using System;
using System.Net;

namespace NTwitch
{
    public class HttpException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public string Reason { get; }

        public HttpException(HttpStatusCode code, string reason = null)
            : base($"{code} {reason}")
        {
            StatusCode = code;
            Reason = reason;
        }
    }
}
