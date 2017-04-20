using System;
using System.Linq.Expressions;

namespace WebApiTemplateProject.Utilities.Guard
{
    /// <summary>
    /// Used to perform input validations and generate .NET exceptions upon validation failure.
    /// </summary>
    public static class NetInputGuard
    {
        public static void ThrowArgumentNullExceptionIfNull<TParam>(string parametername, TParam parameter)
        {
            if (parameter == null) throw new ArgumentNullException(parametername);
        }

        public static void ThrowArgumentExceptionIfStringIsNullOrWhitespace(string parametername, string parameter)
        {
            GenericInputGuard.ThrowExceptionIfStringIsNullOrWhitespace<ArgumentException>(parametername, parameter);
        }

        public static void ThrowArgumentExceptionIfNegativeValue(string parametername, double parameter)
        {
            GenericInputGuard.ThrowExceptionIfNegativeValue<ArgumentException>(parametername, parameter);
        }

        public static void ThrowArgumentExceptionIfDefaultValue<TParam>(string parametername, TParam parameter)
            where TParam : IEquatable<TParam>
        {
            GenericInputGuard.ThrowExceptionIfDefaultValue<TParam, ArgumentException>(parametername, parameter);
        }

        public static void ThrowArgumentExceptionIfNotDefaultValue<TParam>(string parametername, TParam parameter)
        where TParam : IEquatable<TParam>
        {
            GenericInputGuard.ThrowExceptionIfNotDefaultValue<TParam, ArgumentException>(parametername, parameter);
        }

        public static void ThrowArgumentExceptionIf<TParam>(string parametername, TParam parameter, Expression<Func<TParam, bool>> parameterValidator)
        {
            GenericInputGuard.ThrowExceptionIf<TParam, ArgumentException>(parametername, parameter, parameterValidator);
        }
    }
}
