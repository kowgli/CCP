using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCP.UnitTests.Executors
{
    public class OperationForExecutionTest1 : IOperation
    {
        public static int RunCount = 0;

        public void Run()
        {
            RunCount++;
        }
    }
}
