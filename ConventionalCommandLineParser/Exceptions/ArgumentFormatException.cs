using System;

namespace ConventionalCommandLineParser.Exceptions
{
    public class ArgumentFormatException : Exception
    {
        public ArgumentFormatException() : base()
        {
        }

        public ArgumentFormatException(string message) : base(message)
        {
        }

        public ArgumentFormatException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}