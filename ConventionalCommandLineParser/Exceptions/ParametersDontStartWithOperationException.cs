using System;

namespace CCP.Exceptions
{
    public class ParametersDontStartWithOperationException : Exception
    {
        public ParametersDontStartWithOperationException() : base()
        {
        }

        public ParametersDontStartWithOperationException(string message) : base(message)
        {
        }

        public ParametersDontStartWithOperationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}