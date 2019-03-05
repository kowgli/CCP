using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConventionalCommandLineParser.UnitTests.Executors
{
    public class CommandForExecutionTest1 : IExecutable
    {
        public static int RunCount = 0;

        public void Run()
        {
            RunCount++;
        }
    }
}
