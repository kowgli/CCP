using System;

namespace CCP.UnitTests.Operations
{
    public class OperationWithGuidProp : IOperation
    {
        public Guid Id { get; set; }

        public void Run()
        {
            
        }
    }
}