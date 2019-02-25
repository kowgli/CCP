using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ConventionalCommandLineParser.UnitTests.MockExecutors
{
    public static class ExecutionState
    {
        public static ConcurrentBag<ExecutionLog> Executions { get; } = new ConcurrentBag<ExecutionLog>();

        public static void AddExecution(string command, Dictionary<string, object> args = null) => Executions.Add(new ExecutionLog(command, args));
    }
}