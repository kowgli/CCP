using System;

namespace CCP.Exceptions
{
    public class ValueParsingException : Exception
    {
        public ValueParsingException()
        {
        }

        public ValueParsingException(string message) : base(message)
        {
        }

        public ValueParsingException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}