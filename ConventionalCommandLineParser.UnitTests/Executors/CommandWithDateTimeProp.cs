using System;

namespace ConventionalCommandLineParser.UnitTests.Executors
{
    public class CommandWithDateTimeProp : IExecutable
    {
        public DateTime Arg1 { get; set; }

        public void Run()
        {
        }
    }
}