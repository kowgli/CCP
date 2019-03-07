namespace BasicNetFrameworkSample
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                CCP.Executor.ExecuteFromArgs(args, typeof(Program).Assembly);
            }
            catch(CCP.CommandParsingException cpe)
            {
                System.Console.WriteLine(cpe.Message);
            }
        }
    }
}