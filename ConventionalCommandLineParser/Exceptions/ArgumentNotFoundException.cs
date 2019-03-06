using System;

namespace CCP.Exceptions
{
    public class ArgumentNotFoundException : Exception
    {
        public string OperationName { get; private set; }
        public string ArgumentName { get; private set; }    

        public ArgumentNotFoundException(string operationName, string argumentName)
        {
            this.OperationName = operationName ?? "";
            this.ArgumentName = argumentName ?? "";
        }

        public override string Message => $"The argument {ArgumentName} is invalid for the operation {OperationName}.";
    }
}