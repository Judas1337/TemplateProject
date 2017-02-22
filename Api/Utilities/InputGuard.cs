using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Api.Utilities
{
    public static class InputGuard
    {
        public static void ThrowArgumentExceptionIfNegativeValue(string parameterName, int value)
        {
            ThrowArgumentExceptionIf(parameterName, value, (param) => param < 0);
        }

        public static void ThrowArgumentExceptionIfInvalidString(string parameterName, string str)
        {
            Expression<Func<string, bool>> validationExpression = (param) => string.IsNullOrWhiteSpace(param);

            string exceptionMessage;
            if (ParamIsInvalid(parameterName, str, validationExpression, out exceptionMessage))
            {
                throw new ArgumentException(exceptionMessage);
            }
        }

        public static void ThrowArgumentExceptionIfInvalidGuid(string parameterName, string guidAsString)
        {
            Expression<Func<string, bool>> validationExpression = (param) => string.IsNullOrWhiteSpace(param);

            string exceptionMessage;
            if (ParamIsInvalid(parameterName, guidAsString, validationExpression, out exceptionMessage))
            {
                throw new ArgumentException(exceptionMessage);
            }
            else if (ParamIsInvalid(parameterName, guidAsString, (param) => IsInvalidGuid(param), out exceptionMessage))
            {
                throw new ArgumentException(exceptionMessage);
            }

        }

        public static void ThrowArgumentExceptionIfInvalidDate(string parametername, string dateAsString)
        {
            ThrowArgumentExceptionIf(parametername, dateAsString, (param) => IsInvalidDate(param));
        }

        public static void ThrowArgumentExceptionIfInvalidDateInterval(string fromDateParametername,
            string toDateParametername, string fromDateAsString, string toDateAsString)
        {
            ThrowArgumentExceptionIfInvalidDate(fromDateParametername, fromDateAsString);

            if (toDateAsString == null) return;
            ThrowArgumentExceptionIfInvalidDate(toDateParametername, toDateAsString);

            var fromDate = DateTime.Parse(fromDateAsString);
            var toDate = DateTime.Parse(toDateAsString);
            if (fromDate > toDate)
            {
                var condition = $"{fromDateAsString} > {toDateAsString}";
                var exceptionMessage = GenerateExceptionMessge(fromDateParametername, fromDateAsString, condition);
                throw new ArgumentException(exceptionMessage);
            }
        }

        public static void ThrowArgumentNullExceptionIfNull<T>(string parametername, T param)
        {
            if (param == null)
            {
                throw new ArgumentNullException(parametername);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parametername">The name of the variable that's being validated</param>
        /// <param name="parameter">The parameter used in the validation</param>
        /// <param name="parameterValidator">A expression containing a function that will return true if the parameter is invalid</param>
        public static void ThrowArgumentExceptionIf<T>(string parametername, T parameter, Expression<Func<T, bool>> parameterValidator)
        {
            string exceptionMessage;
            if (ParamIsInvalid(parametername, parameter, parameterValidator, out exceptionMessage))
            {
                throw new ArgumentException(exceptionMessage);
            }
        }

        private static bool ParamIsInvalid<T>(string parametername, T param, Expression<Func<T, bool>> paramInvalidator, out string message)
        {
            message = null;
            if (!paramInvalidator.Compile()(param)) return false;
            message = GenerateInvalidParamMessage(parametername, param, paramInvalidator);
            return true;
        }

        private static string GenerateInvalidParamMessage<T>(string parametername, T param, Expression<Func<T, bool>> paramInvalidator)
        {
            var condition = paramInvalidator.ToString();
            var localparameter = condition.Substring(0, condition.IndexOf("=>", StringComparison.Ordinal)).Trim();
            condition = paramInvalidator.Body.ToString();
            condition = condition.Replace(localparameter, parametername);
            var message = GenerateExceptionMessge(parametername, param, condition);
            return message;
        }

        private static string GenerateExceptionMessge<T>(string parametername, T param, string condition)
        {
            return $"Parameter '{parametername}' with value '{param}' is not valid due to condition: {condition}";
        }
        private static bool IsInvalidGuid(string guidAsString)
        {
            Guid guid;
            return !Guid.TryParse(guidAsString, out guid);
        }
        private static bool IsInvalidDate(string dateAsString)
        {
            DateTime date;
            var validDate = DateTime.TryParse(dateAsString, out date);
            return !validDate;
        }

    }
}