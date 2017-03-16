﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WebApiTemplateProject.Utilities.Guard
{
    /// <summary>
    /// Used to perform input validations and generate .NET exceptions upon validation failure.
    /// </summary>
    public static class NetInputGuard
    {
        public static void ThrowArgumentExceptionIfNegativeValue(string parametername, int parameter)
        {
            GenericInputGuard.ThrowExceptionIfNegativeValue<ArgumentException>(parametername, parameter);
        }

        public static void ThrowArgumentExceptionIfStringIsNullOrWhitespace(string parametername, string parameter)
        {
            GenericInputGuard.ThrowExceptionIf<string, ArgumentException>(parametername, parameter, (param) => string.IsNullOrWhiteSpace(param));
        }

        public static void ThrowArgumentNullExceptionIfNull<TParam>(string parametername, TParam parameter)
        {
            if(parameter == null) throw new ArgumentNullException(parametername);
        }

        public static void ThrowArgumentExceptionIf<TParam>(string parametername, TParam parameter, Expression<Func<TParam, bool>> parameterValidator)
        {
            GenericInputGuard.ThrowExceptionIf<TParam, ArgumentException>(parametername, parameter, parameterValidator);
        }
    }
}