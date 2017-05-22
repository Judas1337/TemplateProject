using System;
using System.Linq;
using System.Linq.Expressions;

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
    }
}
