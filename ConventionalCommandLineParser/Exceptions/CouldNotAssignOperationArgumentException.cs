using System;

namespace CCP.Exceptions
{
    public class CouldNotAssignOperationArgumentException : Exception
    {
        public string ExecutableName { get; set; } = "";
        public string ParameterName { get; set; } = "";

        public CouldNotAssignOperationArgumentException() : base()
        {
        }

        public CouldNotAssignOperationArgumentException(string message) : base(message)
        {
        }

        public CouldNotAssignOperationArgumentException(string message, string executableName, string parameterName) : base(message)
        {
            this.ExecutableName = executableName ?? "";
            this.ParameterName = parameterName ?? "";
        }

        public CouldNotAssignOperationArgumentException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public CouldNotAssignOperationArgumentException(string message, Exception innerException, string executableName, string parameterName) : base(message, innerException)
        {
            this.ExecutableName = executableName ?? "";
            this.ParameterName = parameterName ?? "";
        }
    }
}