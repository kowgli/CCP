using CCP.Attributes;

namespace CCP.UnitTests.Operations
{
    public class OperationWithAliasedProps : IOperation
    {
        [Alias(Name = "n")]
        [Alias(Name = "n2")]
        public string Name { get; set; }

        [Alias(Name = "nr")]
        [Alias(Name = "nmbr")]
        public int Number { get; set; }

        public void Run()
        {
        }
    }
}