using System;

namespace CCP.Exceptions
{
    public class MultipleArgumentsFoundException : Exception
    {
        public string PropertyName { get; set; } = "";

        public MultipleArgumentsFoundException() : base()
        {
        }

        public MultipleArgumentsFoundException(string message) : base(message)
        {
        }

        public MultipleArgumentsFoundException(string message, string propertyName) : base(message)
        {
            this.PropertyName = propertyName ?? "";
        }

        public MultipleArgumentsFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public MultipleArgumentsFoundException(string message, Exception innerException, string propertyName) : base(message, innerException)
        {
            this.PropertyName = propertyName ?? "";
        }
    }
}