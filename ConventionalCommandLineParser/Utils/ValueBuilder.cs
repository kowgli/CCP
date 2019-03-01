using ConventionalCommandLineParser.Exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace ConventionalCommandLineParser.Utils
{
    internal static class ValueBuilder
    {
        private static CultureInfo locale = new CultureInfo("en-US");            

        private static Dictionary<Type, Func<string, object>> parsers = new Dictionary<Type, Func<string, object>>
        {
            { typeof(string), s => s },
            { typeof(int), s => int.Parse(s, locale) },
            { typeof(long), s => long.Parse(s, locale) },
            { typeof(decimal), s => decimal.Parse(s, locale) },
            { typeof(double), s => double.Parse(s, locale) },
            { typeof(float), s => float.Parse(s, locale) }
        };

        public static object GetValueFromString(Type type, string value)
        {
            if (!parsers.ContainsKey(type))
            {
                throw new TypeNotSupportedException(message: "Not supported", type: type);
            }

            return parsers[type](value);
        }
    }
}