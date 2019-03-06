using System;

namespace CCP.Exceptions
{
    public class CommandParsingException : Exception
    {
        public CommandParsingException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}