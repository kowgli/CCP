using System;

namespace CCP.Exceptions
{
    public class TypeNotSupportedException : Exception
    {
        public Type Type { get; set; } = typeof(object);

        public TypeNotSupportedException() : base()
        {
        }

        public TypeNotSupportedException(string message) : base(message)
        {
        }

        public TypeNotSupportedException(string message, Type type) : base(message)
        {
            this.Type = type;
        }

        public TypeNotSupportedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public TypeNotSupportedException(string message, Exception innerException, Type type) : base(message, innerException)
        {
            this.Type = type ?? typeof(object);
        }
    }
}