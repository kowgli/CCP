using ConventionalCommandLineParser.UnitTests.Types;

namespace ConventionalCommandLineParser.UnitTests.Executors
{
    public class CommandWithComplexArg : IExecutable
    {
        public SampleComplexType Arg1 { get; set; }

        public void Run()
        {
        }
    }
}