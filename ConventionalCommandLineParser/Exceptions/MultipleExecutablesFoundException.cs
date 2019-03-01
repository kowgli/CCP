using System;

namespace ConventionalCommandLineParser.Exceptions
{
    public class MultipleExecutablesFoundException : Exception
    {
        public string ExecutableName { get; set; } = "";

        public MultipleExecutablesFoundException() : base()
        {
        }

        public MultipleExecutablesFoundException(string message) : base(message)
        {
        }

        public MultipleExecutablesFoundException(string message, string executableName) : base(message)
        {
            this.ExecutableName = executableName ?? "";
        }

        public MultipleExecutablesFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public MultipleExecutablesFoundException(string message, Exception innerException, string executableName) : base(message, innerException)
        {
            this.ExecutableName = executableName ?? "";
        }
    }
}