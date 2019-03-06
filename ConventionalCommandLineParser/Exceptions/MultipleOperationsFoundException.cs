using System;

namespace CCP.Exceptions
{
    public class MultipleOperationsFoundException : Exception
    {
        public string OperationName { get; private set; }

      
        public MultipleOperationsFoundException(string operationName)
        {
            this.OperationName = operationName ?? "";
        }

        public override string Message => $"Multiple operations named {OperationName} found.";
    }
}