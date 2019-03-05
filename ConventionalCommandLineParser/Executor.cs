using CCP.Exceptions;
using CCP.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CCP
{
    public static class Executor
    {       
        public static void ExecuteFromArgs(string[] args, Assembly executableAssembly, FormattingOptions? formattingOptions = null)
        {
            formattingOptions = formattingOptions ?? FormattingOptions.Default;

            executableAssembly = executableAssembly ?? throw new ArgumentNullException(nameof(executableAssembly));
            args = args ?? throw new ArgumentNullException(nameof(args));

            IOperation[] executables = BuildOperations(args, executableAssembly, formattingOptions);
            ExecuteExecutables(executables);
        }

        internal static IOperation[] BuildOperations(string[] args, Assembly executableAssembly, FormattingOptions formattingOptions)
        {
            List<IOperation> result = new List<IOperation>();

            TypeInfo[] executableTypes = executableAssembly.DefinedTypes
                                                           .Where(x => x.ImplementedInterfaces.Contains(typeof(IOperation)))
                                                           .ToArray();

            Operation[] parsedArguments = Utils.ArgumentsParser.Parse(args);

            Utils.OperationBuilder executableBuilder = new Utils.OperationBuilder(formattingOptions);

            foreach(Operation parsedArgument in parsedArguments)
            {
                IOperation instance = executableBuilder.CreateInstance(executableTypes, parsedArgument);

                result.Add(instance);
            }

            return result.ToArray();
        }

        internal static void ExecuteExecutables(IOperation[] executables)
        {
            foreach(IOperation executable in executables)
            {
                executable.Run();
            }
        }
    }
}
