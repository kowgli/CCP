using System;

namespace ConventionalCommandLineParser.Exceptions
{
    public class PropertyNotFoundException : Exception
    {
        public string PropertyName { get; set; } = "";

        public PropertyNotFoundException() : base()
        {
        }

        public PropertyNotFoundException(string message) : base(message)
        {
        }

        public PropertyNotFoundException(string message, string propertyName) : base(message)
        {
            this.PropertyName = propertyName ?? "";
        }

        public PropertyNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public PropertyNotFoundException(string message, Exception innerException, string propertyName) : base(message, innerException)
        {
            this.PropertyName = propertyName ?? "";
        }
    }
}