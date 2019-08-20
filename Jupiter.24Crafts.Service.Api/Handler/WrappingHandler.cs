using Jupiter._24Crafts.Service.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Jupiter._24Crafts.Service.Api.Handler
{
    public class WrappingHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
          CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);
            return BuildApiResponse(request, response);
        }

        private static HttpResponseMessage BuildApiResponse(HttpRequestMessage request, HttpResponseMessage response)
        {
            object content;
            string errorMessage = null;
            if (response.TryGetContentValue(out content) && !response.IsSuccessStatusCode)
            {
                var error = content as HttpError;

                if (error != null)
                {
                    content = null;
                    errorMessage = error.Message;
                    #if DEBUG
                    errorMessage = string.Concat(errorMessage, error.ExceptionMessage, error.StackTrace);
                    #endif
                }
            }
            
            content = content == null ? response.Content : content;

            var newResponse = request.CreateResponse(response.StatusCode,
                new ResponseWrapper(response.StatusCode, content, errorMessage));
            foreach (var header in response.Headers)
            {
                newResponse.Headers.Add(header.Key, header.Value);
            }
            return newResponse;
        }
    }
}