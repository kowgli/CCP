using System;

namespace ConventionalCommandLineParser.Exceptions
{
    public class MultiplePropertiesFoundException : Exception
    {
        public string PropertyName { get; set; } = "";

        public MultiplePropertiesFoundException() : base()
        {
        }

        public MultiplePropertiesFoundException(string message) : base(message)
        {
        }

        public MultiplePropertiesFoundException(string message, string propertyName) : base(message)
        {
            this.PropertyName = propertyName ?? "";
        }

        public MultiplePropertiesFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public MultiplePropertiesFoundException(string message, Exception innerException, string propertyName) : base(message, innerException)
        {
            this.PropertyName = propertyName ?? "";
        }
    }
}