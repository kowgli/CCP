using CCP;
using CCP.UnitTests.Types;

namespace ConventionalCommandLineParser.UnitTests.Operations
{
    public class OperationWithArrayProps : IOperation
    {
        public string[] StringArray { get; set; }

        public int[] IntArray { get; set; }

        public SampleComplexType[] ComplexArray { get; set;}

        public void Run()
        {
            
        }
    }
}