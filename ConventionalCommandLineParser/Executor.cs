using ConventionalCommandLineParser.Exceptions;
using ConventionalCommandLineParser.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConventionalCommandLineParser
{
    public static class Executor
    {       
        public static void ExecuteFromArgs(string[] args, Assembly executableAssembly, FormattingOptions? formattingOptions = null)
        {
            formattingOptions = formattingOptions ?? FormattingOptions.Default;

            executableAssembly = executableAssembly ?? throw new ArgumentNullException(nameof(executableAssembly));
            args = args ?? throw new ArgumentNullException(nameof(args));

            IExecutable[] executables = BuildExecutables(args, executableAssembly, formattingOptions);
            ExecuteExecutables(executables);
        }

        internal static IExecutable[] BuildExecutables(string[] args, Assembly executableAssembly, FormattingOptions formattingOptions)
        {
            List<IExecutable> result = new List<IExecutable>();

            TypeInfo[] executableTypes = executableAssembly.DefinedTypes
                                                           .Where(x => x.ImplementedInterfaces.Contains(typeof(IExecutable)))
                                                           .ToArray();

            Command[] parsedArguments = Utils.ArgumentsParser.Parse(args);

            Utils.ExecutableBuilder executableBuilder = new Utils.ExecutableBuilder(formattingOptions);

            foreach(Command parsedArgument in parsedArguments)
            {
                IExecutable instance = executableBuilder.CreateInstance(executableTypes, parsedArgument);

                result.Add(instance);
            }

            return result.ToArray();
        }

        internal static void ExecuteExecutables(IExecutable[] executables)
        {
            foreach(IExecutable executable in executables)
            {
                executable.Run();
            }
        }
    }
}
