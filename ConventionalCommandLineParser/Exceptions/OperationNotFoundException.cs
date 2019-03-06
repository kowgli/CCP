using System;

namespace CCP.Exceptions
{
    public class OperationNotFoundException : Exception
    {
        public string OperationName { get; private set; }

        public OperationNotFoundException(string operationName) 
        {
            this.OperationName = operationName ?? "";
        }

        public override string Message => $"Operation name {OperationName} not found.";
    }
}