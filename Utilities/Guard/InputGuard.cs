﻿using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Http.Description;

namespace WebApiTemplateProject.Utilities.Guard
{
    public static class InputGuard
    {
        public static void ThrowExceptionIfStringIsNullOrWhitespace<TException>(string parametername, string parameter) where TException : Exception
        {
            ThrowExceptionIf<string, TException>(parametername, parameter, (param) => string.IsNullOrWhiteSpace(param));
        }

        public static void ThrowExceptionIfNegativeValue<TException>(string parametername, int parameter) where TException : Exception
        {
            ThrowExceptionIf<int, TException>(parametername, parameter, (param) => param < 0);
        }

        public static void ThrowExceptionIfNull<TParam, TException>(string parametername, TParam parameter) where TException : Exception
        {
            ThrowExceptionIf<TParam, TException>(parametername, parameter, (param)=> param == null);
        }

        /// <summary>
        /// Throws an exception of the type specified by <typeparamref name="TException"/> if the expression <paramref name="parameterValidator"/> evaluates to true.
        /// </summary>
        /// <typeparam name="TParam">The type of the variable that's being validated</typeparam>
        /// <typeparam name="TException">The type of exception to be thrown if expression evaluates to true. </typeparam>
        /// <param name="parametername">The name of the variable that's being validated</param>
        /// <param name="parameter">The parameter used in the validation</param>
        /// <param name="parameterValidator">A expression containing a function that will return true if the parameter is invalid</param>
        public static void ThrowExceptionIf<TParam, TException>(string parametername, TParam parameter, Expression<Func<TParam, bool>> parameterValidator) where TException : Exception 
        {
            if (!ParamIsInvalid(parameter, parameterValidator)) return;
            var exception = GenerateException<TParam, TException>(parametername, parameter, parameterValidator);
            throw exception;
        }

        #region Helper methods
        /// <summary>
        /// Evalutes the expression and returns true if the expression evaluates to true
        /// </summary>
        /// <typeparam name="TParam"></typeparam>
        /// <param name="param"></param>
        /// <param name="paramInvalidator"></param>
        /// <returns></returns>
        private static bool ParamIsInvalid<TParam>(TParam param, Expression<Func<TParam, bool>> paramInvalidator)
        {
            return paramInvalidator.Compile()(param);
        }

        /// <summary>
        /// Generates an Exception of the specified <typeparamref name="TException"/> 
        /// </summary>
        /// <typeparam name="TException">The type of exception that is to be generated. </typeparam>
        /// <typeparam name="TParam"></typeparam>
        /// <param name="parameterName">The name of <paramref name="parameter"/></param>
        /// <param name="parameter">The parameter under validation</param>
        /// <param name="parameterValidator">The variable name of <paramref name="parameter"/></param>
        /// <returns>The generated exception of type <typeparamref name="TException"/></returns>
        private static TException GenerateException<TParam, TException>(string parameterName, TParam parameter, Expression<Func<TParam, bool>> parameterValidator) where TException : Exception
        {
            var exceptionconstructorWithMessageParameter = GetExceptionConstructorWithMessageParameter<TException>();

            var exceptionMessage = GenerateExceptionMessage(parameterName, parameter, parameterValidator);
            var exception = (TException)exceptionconstructorWithMessageParameter.Invoke(new object[] {exceptionMessage});

            return exception;
        }

        /// <summary>
        /// Tries to retrieve a constructor for exception subclass <typeparamref name="TException"/> that has one parameter called "message"
        /// </summary>
        /// <typeparam name="TException">A subclass of Exception which must obey Microsofts design guideliens:https://msdn.microsoft.com/en-us/library/seyhszts(v=vs.110).aspx
        /// and thereby have a constructor that takes a single parameter called "message" </typeparam>
        /// <returns>A constructor with a single parameter called "message"</returns>
        private static ConstructorInfo GetExceptionConstructorWithMessageParameter<TException>() where TException : Exception
        {
            const string messageParameter = "message";

            ConstructorInfo exceptionMessageConstructor = null;
            foreach (var constructorInfo in typeof(TException).GetConstructors())
            {
                var parameters = constructorInfo.GetParameters();
                if (parameters.Any() == false) continue;
                if (parameters.Length == 1 && parameters.First().Name == messageParameter)
                {
                    exceptionMessageConstructor = constructorInfo;
                }
            }

            if (exceptionMessageConstructor == null)
                throw new NotSupportedException($"Failed to generate exception of type {typeof(TException).Name}. {typeof(TException).Name} does not have a constructor that accepts {messageParameter} as it's only parameter");

            return exceptionMessageConstructor;
        }

        /// <summary>
        /// Generates an exception message from a expression 
        /// </summary>
        /// <typeparam name="TParam">The type of the <paramref name="param"/></typeparam>
        /// <param name="parametername">The name of the <paramref name="param"/> which is used to replace the actual value of <paramref name="param"/> and replace the value with its name</param>
        /// <param name="param">The parameter being validated</param>
        /// <param name="paramInvalidator">The expression that is going to be converted to an exceptionmessage</param>
        /// <returns></returns>
        private static string GenerateExceptionMessage<TParam>(string parametername, TParam param, Expression<Func<TParam, bool>> paramInvalidator)
        {
            var condition = paramInvalidator.Body.ToString();
            condition = condition.Replace(paramInvalidator.Parameters.First().Name, parametername);
           
            return $"Parameter '{parametername}' with value '{param}' is not valid due to condition: {condition}";
        }
        #endregion
    }
}