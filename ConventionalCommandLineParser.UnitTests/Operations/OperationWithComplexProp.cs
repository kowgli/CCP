using CCP.UnitTests.Types;

namespace CCP.UnitTests.Executors
{
    public class OperationWithComplexProp : IOperation
    {
        public SampleComplexType Arg1 { get; set; }

        public void Run()
        {
        }
    }
}