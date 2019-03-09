using CCP.Exceptions;
using CCP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace CCP.Utils
{
    internal class ValueBuilder
    {
        private CultureInfo locale;
        private string? dateFormat;

        public ValueBuilder(CultureInfo locale, string? dateFormat)
        {
            this.locale = locale ?? throw new ArgumentNullException(nameof(locale));
            this.dateFormat = dateFormat;
        }

        private Dictionary<Type, Func<string, object>> GetDefaultParsers()
        {
            return new Dictionary<Type, Func<string, object>>
            {
                { typeof(string), s => s },
                { typeof(sbyte), s => sbyte.Parse(s, locale)},
                { typeof(sbyte?), s => sbyte.Parse(s, locale)},
                { typeof(short), s => short.Parse(s, locale)},
                { typeof(short?), s => short.Parse(s, locale)},
                { typeof(int), s => int.Parse(s, locale)},
                { typeof(int?), s => int.Parse(s, locale)},
                { typeof(long), s => long.Parse(s, locale) },
                { typeof(long?), s => long.Parse(s, locale) },
                { typeof(byte), s => byte.Parse(s, locale)},
                { typeof(byte?), s => byte.Parse(s, locale)},
                { typeof(ushort), s => ushort.Parse(s, locale)},
                { typeof(ushort?), s => ushort.Parse(s, locale)},
                { typeof(uint), s => uint.Parse(s, locale)},
                { typeof(uint?), s => uint.Parse(s, locale)},
                { typeof(ulong), s => ulong.Parse(s, locale) },
                { typeof(ulong?), s => ulong.Parse(s, locale) },
                { typeof(decimal), s => decimal.Parse(s, locale) },
                { typeof(decimal?), s => decimal.Parse(s, locale) },
                { typeof(float), s => float.Parse(s, locale) },
                { typeof(float?), s => float.Parse(s, locale) },
                { typeof(double), s => double.Parse(s, locale) },
                { typeof(double?), s => double.Parse(s, locale) },
                { typeof(char), s => s[0] },
                { typeof(char?), s => s[0] },
                { typeof(bool), s => bool.Parse(s) },
                { typeof(bool?), s => bool.Parse(s) },
                {
                    typeof(DateTime),
                    s => dateFormat != null ? DateTime.ParseExact(s, dateFormat, locale) : DateTime.Parse(s, locale)
                }
            };
        }

        public object GetValue(Type type, Argument argument)
        {
            try
            {
                if(type.IsArray)
                {
                    return ParseAsArray(type, argument);
                }

                var parsers = GetDefaultParsers();
                if (parsers.ContainsKey(type))
                {
                    return parsers[type](argument.Value);
                }

                if (argument.IsPotentialJson)
                {
                    return JsonConvert.DeserializeObject(argument.Value, type);
                }
            }
            catch(Exception ex)
            {
                throw new ValueParsingException(argumentValue: argument.Value, type: type, innerException: ex);
            }

            throw new TypeNotSupportedException(type);            
        }

        private object ParseAsArray(Type type, Argument argument)
        {
            Type actualType = type.GetElementType();

            Array result = Array.CreateInstance(actualType, argument.Values.Length);

            for(int i = 0; i < argument.Values.Length; i++)
            {
                result.SetValue(GetValue(actualType, new Argument($"{argument.Name}={argument.Values[i]}")), i);
            }

            return result;
        }
    }
}