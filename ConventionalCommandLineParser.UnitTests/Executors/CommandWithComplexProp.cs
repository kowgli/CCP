using ConventionalCommandLineParser.UnitTests.Types;

namespace ConventionalCommandLineParser.UnitTests.Executors
{
    public class CommandWithComplexProp : IExecutable
    {
        public SampleComplexType Arg1 { get; set; }

        public void Run()
        {
        }
    }
}