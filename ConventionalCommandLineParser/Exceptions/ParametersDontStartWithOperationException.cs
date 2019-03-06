using System;

namespace CCP.Exceptions
{
    public class ParametersDontStartWithOperationException : Exception
    {
        public ParametersDontStartWithOperationException()
        {
        }

        public override string Message => "Parameters don't start with an operation name.";
    }
}