using System;

namespace CCP
{
    public class CommandParsingException : Exception
    {
        public CommandParsingException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}