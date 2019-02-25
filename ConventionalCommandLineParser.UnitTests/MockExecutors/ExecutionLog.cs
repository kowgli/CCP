using System.Collections.Generic;

namespace ConventionalCommandLineParser.UnitTests.MockExecutors
{
    public class ExecutionLog
    {
        public ExecutionLog(string command, Dictionary<string, object> args)
        {
            this.Command = command;
            this.Arguments = args ?? new Dictionary<string, object>();
        }

        public string Command { get; set; }

        public Dictionary<string, object> Arguments { get; set; } = new Dictionary<string, object>();
    }
}