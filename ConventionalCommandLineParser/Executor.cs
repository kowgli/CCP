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

            Command[] parsedArguments = ArgumentsParser.Parse(args);

            foreach(Command parsedArgument in parsedArguments)
            {
                TypeInfo executable = executableTypes
                                     .Where(t => t.Name == parsedArgument.Name)
                                     .FirstOrDefault();

                if(executable == null)
                {
                    throw new ExecutableNotFoundException(message: "Not found", executableName: parsedArgument.Name);
                }

                IExecutable instance = (IExecutable)Activator.CreateInstance(executable.AsType());

                result.Add(instance);
            }

            return result.ToArray();
        }

        internal static void ExecuteExecutables(IExecutable[] executables)
        {
            
        }
    }
}
