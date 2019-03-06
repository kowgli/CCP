using System;

namespace CCP.Exceptions
{
    public class ArgumentFormatException : Exception
    {
        public string WronglyFormattedArgument { get; private set; }

        public ArgumentFormatException(string wronglyFormattedArgument)
        {
            this.WronglyFormattedArgument = wronglyFormattedArgument ?? "";
        }

        public override string Message => $"Argument {WronglyFormattedArgument} is not formatted correctly. The correct syntax is Arg=[value].";
    }
}