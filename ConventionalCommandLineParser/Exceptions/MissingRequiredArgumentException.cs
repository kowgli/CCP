using System;

namespace CCP.Exceptions
{
    public class MissingRequiredArgumentException : Exception
    {
        public string ExecutableName { get; set; } = "";
        public string ParameterName { get; set; } = "";

        public MissingRequiredArgumentException() : base()
        {
        }

        public MissingRequiredArgumentException(string message) : base(message)
        {
        }

        public MissingRequiredArgumentException(string message, string executableName, string parameterName) : base(message)
        {
            this.ExecutableName = executableName ?? "";
            this.ParameterName = parameterName ?? "";
        }

        public MissingRequiredArgumentException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public MissingRequiredArgumentException(string message, Exception innerException, string executableName, string parameterName) : base(message, innerException)
        {
            this.ExecutableName = executableName ?? "";
            this.ParameterName = parameterName ?? "";
        }
    }
}