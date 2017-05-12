using System;
using System.Net;
using System.Net.Http;
using Microsoft.Rest;

namespace TemplateProject.Sl.WebApi.Mapper
{
    public static class ExceptionToHttpResponse
    {
        public static HttpResponseMessage ToHttpResponseMessage(Exception exception)
        {
            HttpStatusCode statusCode;
            if (exception is ArgumentException) statusCode = HttpStatusCode.BadRequest;
            else if (exception is NotImplementedException) statusCode = HttpStatusCode.NotImplemented;
            else if (exception is HttpOperationException) statusCode = ((HttpOperationException)exception).Response.StatusCode;
            else statusCode = HttpStatusCode.InternalServerError;

            return new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(exception.Message)
            };
        }
    }
}
