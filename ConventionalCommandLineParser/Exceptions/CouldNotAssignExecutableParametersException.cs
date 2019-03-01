using System;

namespace ConventionalCommandLineParser.Exceptions
{
    public class CouldNotAssignExecutableParametersException : Exception
    {
        public string ExecutableName { get; set; } = "";
        public string ParameterName { get; set; } = "";

        public CouldNotAssignExecutableParametersException() : base()
        {
        }

        public CouldNotAssignExecutableParametersException(string message) : base(message)
        {
        }

        public CouldNotAssignExecutableParametersException(string message, string executableName, string parameterName) : base(message)
        {
            this.ExecutableName = executableName ?? "";
            this.ParameterName = parameterName ?? "";
        }

        public CouldNotAssignExecutableParametersException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public CouldNotAssignExecutableParametersException(string message, Exception innerException, string executableName, string parameterName) : base(message, innerException)
        {
            this.ExecutableName = executableName ?? "";
            this.ParameterName = parameterName ?? "";
        }
    }
}