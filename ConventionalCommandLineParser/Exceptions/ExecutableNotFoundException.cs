using System;

namespace ConventionalCommandLineParser.Exceptions
{
    public class ExecutableNotFoundException : Exception
    {
        public string ExecutableName { get; set; } = "";

        public ExecutableNotFoundException() : base()
        {
        }

        public ExecutableNotFoundException(string message) : base(message)
        {
        }

        public ExecutableNotFoundException(string message, string executableName) : base(message)
        {
            this.ExecutableName = executableName ?? "";
        }

        public ExecutableNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ExecutableNotFoundException(string message, Exception innerException, string executableName) : base(message, innerException)
        {
            this.ExecutableName = executableName ?? "";
        }
    }
}