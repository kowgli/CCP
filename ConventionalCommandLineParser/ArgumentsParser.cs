using ConventionalCommandLineParser.Exceptions;
using ConventionalCommandLineParser.Models;
using System;
using System.Collections.Generic;

namespace ConventionalCommandLineParser
{
    internal static class ArgumentsParser
    {
        public static Command[] Parse(string[] args)
        {
            if(args?.Length == 0)
            {
                return new Command[0];
            }

            List<Command> commands = new List<Command>();
            Command? command = null;

            foreach (string arg in args)
            {
                if(ArgumentIsAction(arg))
                {
                    command = new Command(arg);
                    continue;
                }

                if(command == null)
                {
                    throw new ArgumentsDontStartWithCommandException();
                }

                Argument argument = new Argument(arg);
                command.AddArgument(argument);
            }

            return commands.ToArray();
        }

        private static bool ArgumentIsAction(string argument)
        {
            return argument?.Contains("=") ?? false;
        }
    }
}
