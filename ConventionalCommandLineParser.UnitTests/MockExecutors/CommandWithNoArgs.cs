﻿namespace ConventionalCommandLineParser.UnitTests.MockExecutors
{
    public class CommandWithNoArgs : IExecutable
    {
        public void Run()
        {
            ExecutionState.AddExecution(nameof(CommandWithNoArgs));
        }
    }
}