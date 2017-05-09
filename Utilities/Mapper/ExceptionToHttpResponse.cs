using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Rest;
using WebApiTemplateProject.Utilities.ExceptionHandler;

namespace WebApiTemplateProject.Utilities.Mapper
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
