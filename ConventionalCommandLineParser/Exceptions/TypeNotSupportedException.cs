using System;

namespace CCP.Exceptions
{
    public class TypeNotSupportedException : Exception
    {
        public Type Type { get; private set; }

        public TypeNotSupportedException(Type type) 
        {
            this.Type = type;
        }

        public override string Message => $"The type {Type} is not supported";
    }
}