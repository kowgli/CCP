using System;

namespace ConventionalCommandLineParser.UnitTests.Executors
{
    public class CommandWithDateTimeArg : IExecutable
    {
        public DateTime Arg1 { get; set; }

        public void Run()
        {
        }
    }
}