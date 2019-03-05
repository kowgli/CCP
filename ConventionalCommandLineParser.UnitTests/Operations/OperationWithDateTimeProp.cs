using System;

namespace CCP.UnitTests.Executors
{
    public class OperationWithDateTimeProp : IOperation
    {
        public DateTime Arg1 { get; set; }

        public void Run()
        {
        }
    }
}