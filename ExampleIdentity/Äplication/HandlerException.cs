using System;
using System.Net;

namespace ExampleIdentity.Äplication
{
    public class HandlerException : Exception
    {
        public HttpStatusCode Code { get; }

        public object Error { get; }

        public HandlerException(HttpStatusCode code, object error = null)
        {
            Code = code;
            Error = error;

        }
    }
}
