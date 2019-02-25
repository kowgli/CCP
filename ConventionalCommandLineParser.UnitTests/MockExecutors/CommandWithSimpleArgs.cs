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
            ExecutionState.AddExecution(nameof(CommandWithNoArgs), new System.Collections.Generic.Dictionary<string, object>
            {
                { nameof(Arg1), Arg1 },
                { nameof(Arg2), Arg2 },
                { nameof(Arg3), Arg3 }
            });
        }
    }
}