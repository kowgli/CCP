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
        public static void ExecuteFromArgs(Assembly executableAssembly, string[] args)
        {
            executableAssembly = executableAssembly ?? throw new ArgumentNullException(nameof(executableAssembly));
            args = args ?? throw new ArgumentNullException(nameof(args));

            var types = executableAssembly.DefinedTypes.Where(x => x.ImplementedInterfaces.Contains(typeof(IExecutable)));

        }
    }
}
