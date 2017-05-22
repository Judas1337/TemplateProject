using System;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TemplateProject.Utilities.Guard;

namespace TemplateProject.Sl.WebApi.Guard
{
    /// <summary>
    /// In WebApi, all uncaught exceptions except HttpResponseException are caught by the ExceptionLoggingHandler and ExceptionHandler.
    /// If this is not the intention a HttpResponseException, which is the only exception who will not be caught should be thrown. 
    /// </summary>
    public static class HttpInputGuard
    {
        public static void ThrowBadRequestIfNull<TParam>(string parametername, TParam parameter)
        {
            if (parameter != null) return;

            var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent($"Parameter '{parametername}' with value '{parameter}' is not valid due to condition: {parametername} == null")
            };

            throw new HttpResponseException(response);
        }

        public static void ThrowBadRequestIfStringIsNullOrWhitespace(string parametername, string parameter)
        {
            ThrowHttpResponseExceptionIf(parametername, parameter, HttpStatusCode.BadRequest, (param) => string.IsNullOrWhiteSpace(param));
        }

        public static void ThrowBadRequestIfNegativeValue(string parametername, double parameter)
        {
            ThrowHttpResponseExceptionIf(parametername, parameter, HttpStatusCode.BadRequest, (param) => param < 0);
        }

        public static void ThrowBadRequestIfDefaultValue<TParam>(string parametername, TParam parameter)
            where TParam : IEquatable<TParam>
        {
            ThrowHttpResponseExceptionIf(parametername, parameter, HttpStatusCode.BadRequest, (param) => param.Equals(default(TParam)));
        }

        public static void ThrowBadRequestIfNotDefaultValue<TParam>(string parametername, TParam parameter)
        where TParam : IEquatable<TParam>
        {
            ThrowHttpResponseExceptionIf(parametername, parameter, HttpStatusCode.BadRequest, (param) => !param.Equals(default(TParam)));
        }

        public static void ThrowHttpResponseExceptionIf<TParam>(string parametername, TParam parameter, HttpStatusCode statusCode, Expression<Func<TParam, bool>> parameterValidator)
        {
            if (!GuardUtilities.ParamIsInvalid(parameter, parameterValidator)) return;
            var exceptionMessage = GuardUtilities.GenerateExceptionMessage(parametername, parameter, parameterValidator);

            var response = new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(exceptionMessage)
            };

            throw new HttpResponseException(response);
        }
    }
}
