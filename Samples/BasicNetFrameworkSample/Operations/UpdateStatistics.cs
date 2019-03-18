using CCP;
using System;

namespace BasicNetFrameworkSample.Operations
{
    public class UpdateStatistics : IOperation
    {
        public string Name { get; set; }

        public int[] Numbers { get; set; }

        public void Run()
        {
            Console.WriteLine($"Updating statistic {Name}, with numbers {String.Join(", ", Numbers)}.");
        }
    }
}