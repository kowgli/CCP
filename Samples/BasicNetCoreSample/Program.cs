using System;

namespace BasicNetCoreSample
{
    class Program
    {
        static void Main(string[] args)
        {
            ConventionalCommandLineParser.Executor.ExecuteFromArgs(args, typeof(Program).Assembly);
        }
    }
}
