using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace TemplateProject.Utilities.Guard
{
    public static class GuardUtilities
    {
        /// <summary>
        /// Evalutes the expression and returns true if the expression evaluates to true
        /// </summary>
        /// <typeparam name="TParam"></typeparam>
        /// <param name="param"></param>
        /// <param name="paramInvalidator"></param>
        /// <returns></returns>
        public static bool ParamIsInvalid<TParam>(TParam param, Expression<Func<TParam, bool>> paramInvalidator)
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
        /// <param name="parameterValidator">The validation expression which will be converted to a string and added to the exceptionmessage</param>
        /// <returns>The generated exception of type <typeparamref name="TException"/></returns>
        public static TException GenerateException<TParam, TException>(string parameterName, TParam parameter, Expression<Func<TParam, bool>> parameterValidator) where TException : System.Exception
        {
            var exceptionconstructorWithMessageParameter = GetExceptionConstructorWithMessageParameter<TException>();

            var exceptionMessage = GuardUtilities.GenerateExceptionMessage(parameterName, parameter, parameterValidator);
            var exception = (TException)exceptionconstructorWithMessageParameter.Invoke(new object[] { exceptionMessage });

            return exception;
        }

        /// <summary>
        /// Generates an Exception of the specified <typeparamref name="TException"/> 
        /// </summary>
        /// <typeparam name="TException">The type of exception that is to be generated. </typeparam>
        /// <typeparam name="TParam"></typeparam>
        /// <param name="parameterName">The name of <paramref name="parameter"/></param>
        /// <param name="parameter">The parameter under validation</param>
        /// <param name="condition">The condition which failed causing the exception to be thrown. Will be added to the exception message</param>
        /// <returns>The generated exception of type <typeparamref name="TException"/></returns>
        public static TException GenerateException<TParam, TException>(string parameterName, TParam parameter, string condition) where TException : System.Exception
        {
            var exceptionconstructorWithMessageParameter = GetExceptionConstructorWithMessageParameter<TException>();

            var exceptionMessage = GuardUtilities.GenerateExceptionMessage(parameterName, parameter, condition);
            var exception = (TException)exceptionconstructorWithMessageParameter.Invoke(new object[] { exceptionMessage });

            return exception;
        }

        /// <summary>
        /// Generates an exception message from a expression 
        /// </summary>
        /// <typeparam name="TParam">The type of the <paramref name="param"/></typeparam>
        /// <param name="parametername">The name of the <paramref name="param"/> which is used to replace the actual value of <paramref name="param"/> and replace the value with its name</param>
        /// <param name="param">The parameter being validated</param>
        /// <param name="paramInvalidator">The expression that is going to be converted to an exceptionmessage</param>
        /// <returns></returns>
        public static string GenerateExceptionMessage<TParam>(string parametername, TParam param, Expression<Func<TParam, bool>> paramInvalidator)
        {
            var condition = paramInvalidator.Body.ToString();
            condition = condition.Replace(paramInvalidator.Parameters.First().Name, parametername);
            return GenerateExceptionMessage(parametername, param, condition);
        }


        /// <summary>
        /// Generates an exception message based on the provided parameters 
        /// </summary>
        /// <typeparam name="TParam">The type of the <paramref name="param"/></typeparam>
        /// <param name="parametername">The name of the <paramref name="param"/> which is used to replace the actual value of <paramref name="param"/> and replace the value with its name</param>
        /// <param name="param">The parameter being validated</param>
        /// <param name="condition">The condition why there is an exception</param>
        /// <returns></returns>
        public static string GenerateExceptionMessage<TParam>(string parametername, TParam param, string condition)
        {
            return $"Parameter '{parametername}' with value '{param}' is not valid due to condition: {condition}";
        }

        /// <summary>
        /// Tries to retrieve a constructor for exception subclass <typeparamref name="TException"/> that has one parameter called "message"
        /// </summary>
        /// <typeparam name="TException">A subclass of Exception which must obey Microsofts design guideliens</typeparam>
        /// <returns>A constructor with a single parameter called "message"</returns>
        public static ConstructorInfo GetExceptionConstructorWithMessageParameter<TException>() where TException : System.Exception
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
    }
}
