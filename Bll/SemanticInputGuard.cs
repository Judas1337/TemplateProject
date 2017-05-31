using System;
using System.Linq.Expressions;
using TemplateProject.Bll.Contract.Bll.Model.Exceptions;
using TemplateProject.Utilities.Guard;

namespace TemplateProject.Bll
{
    /// <summary>
    /// Used to perform input validations and generate instances of subclasses to <exception cref="SemanticException"></exception> upon validation failure.
    /// </summary>
    public class SemanticInputGuard
    {
        public static void ThrowInputExceptionIfNull<TParam>(string parametername, TParam parameter)
        {
            GenericInputGuard.ThrowExceptionIfNull<TParam, InputException>(parametername, parameter);
        }

        public static void ThrowInputExceptionIfStringIsNullOrWhitespace(string parametername, string parameter)
        {
            GenericInputGuard.ThrowExceptionIfStringIsNullOrWhitespace<InputException>(parametername, parameter);
        }

        public static void ThrowInputExceptionIfNegativeValue(string parametername, double parameter)
        {
            GenericInputGuard.ThrowExceptionIfNegativeValue<InputException>(parametername, parameter);
        }

        public static void ThrowInputExceptionIfDefaultValue<TParam>(string parametername, TParam parameter)
            where TParam : IEquatable<TParam>
        {
            GenericInputGuard.ThrowExceptionIfDefaultValue<TParam, InputException>(parametername, parameter);
        }

        public static void ThrowInputExceptionIfNotDefaultValue<TParam>(string parametername, TParam parameter)
        where TParam : IEquatable<TParam>
        {
            GenericInputGuard.ThrowExceptionIfNotDefaultValue<TParam, InputException>(parametername, parameter);
        }

        public static void ThrowInputExceptionIfMatchesRegex(string parametername, string parameter, string regex)
        {
            GenericInputGuard.ThrowExceptionIfMatchesRegex<InputException>(parametername, parameter, regex);
        }

        public static void ThrowInputExceptionIfNotMatchesRegex(string parametername, string parameter, string regex)
        {
            GenericInputGuard.ThrowExceptionIfNotMatchesRegex<InputException>(parametername, parameter, regex);
        }

        public static void ThrowInputExceptionIf<TParam>(string parametername, TParam parameter, Expression<Func<TParam, bool>> parameterValidator)
        {
            GenericInputGuard.ThrowExceptionIf<TParam, InputException>(parametername, parameter, parameterValidator);
        }
    }
}
