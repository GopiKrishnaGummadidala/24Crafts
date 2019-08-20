using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Jupiter._24Crafts.Service.Api.Models
{
    public class ResponseWrapper
    {
        public ResponseWrapper(HttpStatusCode statusCode, object result = null, string errorMessage = null)
        {
            StatusCode = (int)statusCode;
            Response = result;
            ErrorMessage = errorMessage;
        }

        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public object Response { get; set; }
    }
}