using System;
using System.Net;

namespace yu_pi.Infrastructure.Errors
{
    public class RestException : Exception
    {
        public object Errors { get; set; }

        public HttpStatusCode Code { get;set; }
        public RestException(HttpStatusCode code, object errors = null)
        {
            Code = code;
            Errors = errors;
        }
    }
}