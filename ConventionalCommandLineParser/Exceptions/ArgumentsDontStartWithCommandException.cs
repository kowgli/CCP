using System;

namespace ConventionalCommandLineParser.Exceptions
{
    public class ArgumentsDontStartWithCommandException : Exception
    {
        public ArgumentsDontStartWithCommandException() : base()
        {
        }

        public ArgumentsDontStartWithCommandException(string message) : base(message)
        {
        }

        public ArgumentsDontStartWithCommandException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}