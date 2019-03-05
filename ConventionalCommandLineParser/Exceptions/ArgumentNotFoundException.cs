using System;

namespace CCP.Exceptions
{
    public class ArgumentNotFoundException : Exception
    {
        public string PropertyName { get; set; } = "";

        public ArgumentNotFoundException() : base()
        {
        }

        public ArgumentNotFoundException(string message) : base(message)
        {
        }

        public ArgumentNotFoundException(string message, string propertyName) : base(message)
        {
            this.PropertyName = propertyName ?? "";
        }

        public ArgumentNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ArgumentNotFoundException(string message, Exception innerException, string propertyName) : base(message, innerException)
        {
            this.PropertyName = propertyName ?? "";
        }
    }
}