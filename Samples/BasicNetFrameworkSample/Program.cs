namespace BasicNetFrameworkSample
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            CCP.Executor.ExecuteFromArgs(args, typeof(Program).Assembly);
        }
    }
}