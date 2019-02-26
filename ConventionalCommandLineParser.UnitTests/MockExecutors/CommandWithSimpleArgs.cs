using System;

namespace ConventionalCommandLineParser.UnitTests.MockExecutors
{
    public class CommandWithSimpleArgs : IExecutable
    {
        public string Arg1 { get; set; }

        public int Arg2 { get; set; }

        public decimal Arg3 { get; set; }

        public void Run()
        {
            
        }
    }
}