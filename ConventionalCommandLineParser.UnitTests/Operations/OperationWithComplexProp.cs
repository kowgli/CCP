using CCP.UnitTests.Types;

namespace CCP.UnitTests.Operations
{
    public class OperationWithComplexProp : IOperation
    {
        public SampleComplexType Arg1 { get; set; }

        public void Run()
        {
        }
    }
}