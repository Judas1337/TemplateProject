using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace TemplateProject.Utilities.Guard
{
    /// <summary>
    /// Used to perform input validations and generate specified exceptions upon validation failure.
    /// </summary>
    /// <remarks>Can generate any exception that obeys Microsofts design guidelines for custom exceptions: https://msdn.microsoft.com/en-us/library/seyhszts(v=vs.110).aspx,
    ///  and thereby any exception that has a constructor that takes a single parameter called message</remarks>
    public static class GenericInputGuard
    {
        public static void ThrowExceptionIfStringIsNullOrWhitespace<TException>(string parametername, string parameter) 
            where TException : Exception
        {
            ThrowExceptionIf<string, TException>(parametername, parameter, (param) => string.IsNullOrWhiteSpace(param));
        }

        ///<remarks>Doesn't use ThrowExceptionIf to avoid strange exception messages due to c# autogenereated types by compiler</remarks>
        public static void ThrowExceptionIfNull<TParam, TException>(string parametername, TParam parameter) 
            where TException : Exception
        {
            if (!GuardUtilities.ParamIsInvalid(parameter, (param) => param == null)) return;
            var exception = GuardUtilities.GenerateException<TParam, TException>(parametername, parameter, $"{parametername} == null");
            throw exception;
        }

        public static void ThrowExceptionIfNotDefaultValue<TParam, TException>(string parametername, TParam parameter)
          where TException : Exception
          where TParam : IEquatable<TParam>
        {
            ThrowExceptionIf<TParam, TException>(parametername, parameter, (param) => !param.Equals(default(TParam)));
        }

        public static void ThrowExceptionIfDefaultValue<TParam, TException>(string parametername, TParam parameter) 
            where TException : Exception
            where TParam : IEquatable<TParam>
        {
           ThrowExceptionIf<TParam, TException>(parametername, parameter, (param) => param.Equals(default(TParam)));
        }

        ///<remarks>Doesn't use ThrowExceptionIf to avoid strange exception messages due to c# autogenereated types by compiler</remarks>
        public static void ThrowExceptionIfMatchesRegex<TException>(string parametername, string parameter, string regex)
          where TException : Exception
        {
            if (!GuardUtilities.ParamIsInvalid(parameter, (param) => Regex.IsMatch(parameter, regex))) return;
            var exception = GuardUtilities.GenerateException<string, TException>(parametername, parameter, $"Regex.isMatch({parametername}, {regex}) == true");

            throw exception;
        }

        ///<remarks>Doesn't use ThrowExceptionIf to avoid strange exception messages due to c# autogenereated types by compiler</remarks>
        public static void ThrowExceptionIfNotMatchesRegex<TException>(string parametername, string parameter, string regex)
            where TException : Exception
        {
            if (!GuardUtilities.ParamIsInvalid(parameter, (param) => !Regex.IsMatch(parameter, regex))) return;
            var exception = GuardUtilities.GenerateException<string, TException>(parametername, parameter, $"Regex.isMatch({parametername}, {regex}) != true");

            throw exception;
        }

        /// <remarks>double is used as a parameter type since almost all numeric types(except decimal) can be implicitly converted to double</remarks>
        public static void ThrowExceptionIfNegativeValue<TException>(string parametername, double parameter)
            where TException : Exception
        {
            ThrowExceptionIf<double, TException>(parametername, parameter, (param) => param < 0);
        }

        /// <summary>
        /// Throws an exception of the type specified by <typeparamref name="TException"/> if the expression <paramref name="parameterValidator"/> evaluates to true.
        /// </summary>
        /// <typeparam name="TParam">The type of the variable that's being validated</typeparam>
        /// <typeparam name="TException">The type of exception to be thrown if expression evaluates to true. </typeparam>
        /// <param name="parametername">The name of the variable that's being validated</param>
        /// <param name="parameter">The parameter used in the validation</param>
        /// <param name="parameterValidator">A expression containing a function that will return true if the parameter is invalid</param>
        public static void ThrowExceptionIf<TParam, TException>(string parametername, TParam parameter, Expression<Func<TParam, bool>> parameterValidator)
            where TException : System.Exception 
        {
            if (!GuardUtilities.ParamIsInvalid(parameter, parameterValidator)) return;
            var exception = GuardUtilities.GenerateException<TParam, TException>(parametername, parameter, parameterValidator);
            throw exception;
        }
    }
}