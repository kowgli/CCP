namespace BasicNetFrameworkSample
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ConventionalCommandLineParser.Executor.ExecuteFromArgs(args, typeof(Program).Assembly);
        }
    }
}