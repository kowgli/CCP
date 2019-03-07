using CCP.Exceptions;
using CCP.Models;
using CCP.Utils;
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
        public static void ExecuteFromArgs(string[] args, Assembly opearationsAssembly, Options? options = null)
        {
            options = options ?? Options.Default;

            opearationsAssembly = opearationsAssembly ?? throw new ArgumentNullException(nameof(opearationsAssembly));
            args = args ?? throw new ArgumentNullException(nameof(args));

            IOperation[]? operations = null;
            try
            {
                operations = BuildOperations(args, opearationsAssembly, options);

                if(operations.Length == 0)
                {
                    throw new NoOperationsException();
                }
            }
            catch(Exception ex)
            {
                string helpText = HelpTextBuilder.BuildHelpText(opearationsAssembly, ex);
                throw new CommandParsingException(helpText, ex);
            }

            ExecuteOperations(operations ?? new IOperation[0]);
        }

        internal static IOperation[] BuildOperations(string[] args, Assembly operationsAssembly, Options options)
        {
            List<IOperation> result = new List<IOperation>();

            TypeInfo[] operationTypes = operationsAssembly.DefinedTypes
                                                           .Where(x => x.ImplementedInterfaces.Contains(typeof(IOperation)))
                                                           .ToArray();

            Operation[] parsedArguments = Utils.ArgumentsParser.Parse(args);

            Utils.OperationBuilder operationBuilder = new Utils.OperationBuilder(options);

            foreach(Operation parsedArgument in parsedArguments)
            {
                IOperation instance = operationBuilder.CreateInstance(operationTypes, parsedArgument);

                result.Add(instance);
            }

            return result.ToArray();
        }

        internal static void ExecuteOperations(IOperation[] operations)
        {
            foreach(IOperation operation in operations)
            {
                operation.Run();
            }
        }
    }
}
