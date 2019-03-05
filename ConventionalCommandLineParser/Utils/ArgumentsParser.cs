using CCP.Exceptions;
using CCP.Models;
using System;
using System.Collections.Generic;

namespace CCP.Utils
{
    internal static class ArgumentsParser
    {
        public static Operation[] Parse(string[] args)
        {
            if(args?.Length == 0)
            {
                return new Operation[0];
            }

            List<Operation> commands = new List<Operation>();
            Operation? command = null;

            foreach (string arg in args)
            {
                if(ArgumentIsCommand(arg))
                {
                    command = new Operation(arg);
                    commands.Add(command);
                    continue;
                }

                if(command == null)
                {
                    throw new ParametersDontStartWithOperationException();
                }

                Argument argument = new Argument(arg);
                command.AddArgument(argument);
            }

            return commands.ToArray();
        }

        private static bool ArgumentIsCommand(string argument)
        {
            argument = argument ?? throw new ArgumentNullException(nameof(argument));

            return !argument.Contains("=");
        }
    }
}
