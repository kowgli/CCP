using System;

namespace ConventionalCommandLineParser.Exceptions
{
    public class MissingRequiredParameterException : Exception
    {
        public string ExecutableName { get; set; } = "";
        public string ParameterName { get; set; } = "";

        public MissingRequiredParameterException() : base()
        {
        }

        public MissingRequiredParameterException(string message) : base(message)
        {
        }

        public MissingRequiredParameterException(string message, string executableName, string parameterName) : base(message)
        {
            this.ExecutableName = executableName ?? "";
            this.ParameterName = parameterName ?? "";
        }

        public MissingRequiredParameterException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public MissingRequiredParameterException(string message, Exception innerException, string executableName, string parameterName) : base(message, innerException)
        {
            this.ExecutableName = executableName ?? "";
            this.ParameterName = parameterName ?? "";
        }
    }
}