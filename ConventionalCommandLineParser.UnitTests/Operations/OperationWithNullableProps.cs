using System;

namespace CCP.UnitTests.Operations
{
    public class OperationWithNullableProps : IOperation
    {
        public int? SomeInt { get; set; }
        public Guid? SomeGuid { get; set; }
        public DateTime? SomeDate { get; set; }

        public void Run()
        {            
        }
    }
}