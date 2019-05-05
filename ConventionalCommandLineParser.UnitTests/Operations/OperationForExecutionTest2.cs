using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCP.UnitTests.Operations
{
    public class OperationForExecutionTest2 : IOperation
    {
        public static int RunCount = 0;

        public void Run()
        {
            RunCount++;
        }
    }
}
