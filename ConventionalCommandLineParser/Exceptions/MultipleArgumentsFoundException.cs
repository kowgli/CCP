using System;

namespace CCP.Exceptions
{
    public class MultipleArgumentsFoundException : Exception
    {
        public string OperationName { get; private set; }
        public string ArgumentName { get; private set; }

        public MultipleArgumentsFoundException(string operationName, string argumentName) 
        {
            this.OperationName = operationName ?? "";
            this.ArgumentName = argumentName ?? "";
        }

        public override string Message => $"Argument {ArgumentName} on operation {OperationName} specified multiple times.";
    }
}