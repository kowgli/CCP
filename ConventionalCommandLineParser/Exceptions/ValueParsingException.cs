using System;

namespace CCP.Exceptions
{
    public class ValueParsingException : Exception
    {
        public string ArgumentValue { get; private set; }
        public Type Type { get; private set; }

        public ValueParsingException(string argumentValue, Type type, Exception innerException) : base("", innerException)
        {
            this.ArgumentValue = argumentValue;
            this.Type = type;
        }

        public override string Message => $"Could not parse value \"{ArgumentValue}\" as {Type}.";
    }
}