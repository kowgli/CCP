using System;

namespace CCP.Exceptions
{
    public class MissingRequiredArgumentException : Exception
    {
        public string OperationName { get; private set; }
        public string ArgumentName { get; private set; }

        public MissingRequiredArgumentException( string operationName, string parameterName) 
        {
            this.OperationName = operationName ?? "";
            this.ArgumentName = parameterName ?? "";
        }

        public override string Message => $"The operation {OperationName} is missing the required argument {ArgumentName}";
    }
}