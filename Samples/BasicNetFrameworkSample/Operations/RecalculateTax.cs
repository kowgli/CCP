using BasicNetFrameworkSample.Models;
using CCP;
using CCP.Attributes;
using System;

namespace BasicNetCoreSample.Operations
{
    public class RecalculateTax : IOperation
    {
        [Required]
        public int YearFrom { get; set; }

        public int? YearTo { get; set; }

        public Person Person { get; set; }

        public void Run()
        {
            Console.WriteLine($"Running RecalculateTax with YearFrom={YearFrom}, YearTo={YearTo} Person={Person}");
        }
    }
}