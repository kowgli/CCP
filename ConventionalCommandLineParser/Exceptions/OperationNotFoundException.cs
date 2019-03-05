using System;

namespace CCP.Exceptions
{
    public class OperationNotFoundException : Exception
    {
        public string ExecutableName { get; set; } = "";

        public OperationNotFoundException() : base()
        {
        }

        public OperationNotFoundException(string message) : base(message)
        {
        }

        public OperationNotFoundException(string message, string executableName) : base(message)
        {
            this.ExecutableName = executableName ?? "";
        }

        public OperationNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public OperationNotFoundException(string message, Exception innerException, string executableName) : base(message, innerException)
        {
            this.ExecutableName = executableName ?? "";
        }
    }
}