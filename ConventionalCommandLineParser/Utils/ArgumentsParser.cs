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

            List<Operation> operations = new List<Operation>();
            Operation? operation = null;

            foreach (string arg in args)
            {
                if(ArgumentIsCommand(arg))
                {
                    operation = new Operation(arg);
                    operations.Add(operation);
                    continue;
                }

                if(operation == null)
                {
                    throw new ParametersDontStartWithOperationException();
                }

                Argument argument = new Argument(arg);
                operation.AddArgument(argument);
            }

            return operations.ToArray();
        }

        private static bool ArgumentIsCommand(string argument)
        {
            argument = argument ?? throw new ArgumentNullException(nameof(argument));

            return !argument.Contains("=");
        }
    }
}
