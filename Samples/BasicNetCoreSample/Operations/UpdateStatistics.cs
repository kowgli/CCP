using CCP;
using System;

namespace BasicNetCoreSample.Operations
{
    public class UpdateStatistics : IOperation
    {
        public string Name { get; set; }

        public void Run()
        {
            Console.WriteLine($"Updating statistic {Name}");
        }
    }
}