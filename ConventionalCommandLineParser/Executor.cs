using ConventionalCommandLineParser.Exceptions;
using ConventionalCommandLineParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConventionalCommandLineParser
{
    public static class Executor
    { 
        public static void ExecuteFromArgs(string[] args, Assembly executableAssembly)
        {
            executableAssembly = executableAssembly ?? throw new ArgumentNullException(nameof(executableAssembly));
            args = args ?? throw new ArgumentNullException(nameof(args));

            IExecutable[] executables = BuildExecutables(args, executableAssembly);
            ExecuteExecutables(executables);
        }

        internal static IExecutable[] BuildExecutables(string[] args, Assembly executableAssembly)
        {
            List<IExecutable> result = new List<IExecutable>();

            TypeInfo[] executableTypes = executableAssembly.DefinedTypes
                                                           .Where(x => x.ImplementedInterfaces.Contains(typeof(IExecutable)))
                                                           .ToArray();

            Command[] parsedArguments = Utils.ArgumentsParser.Parse(args);

            foreach(Command parsedArgument in parsedArguments)
            {
                IExecutable instance = Utils.ExecutableBuilder.CreateInstance(executableTypes, parsedArgument);

                result.Add(instance);
            }

            return result.ToArray();
        }


        internal static void ExecuteExecutables(IExecutable[] executables)
        {
            
        }
    }
}
