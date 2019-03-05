using System;

namespace CCP.Exceptions
{
    public class MultipleOperationsFoundException : Exception
    {
        public string ExecutableName { get; set; } = "";

        public MultipleOperationsFoundException() : base()
        {
        }

        public MultipleOperationsFoundException(string message) : base(message)
        {
        }

        public MultipleOperationsFoundException(string message, string executableName) : base(message)
        {
            this.ExecutableName = executableName ?? "";
        }

        public MultipleOperationsFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public MultipleOperationsFoundException(string message, Exception innerException, string executableName) : base(message, innerException)
        {
            this.ExecutableName = executableName ?? "";
        }
    }
}