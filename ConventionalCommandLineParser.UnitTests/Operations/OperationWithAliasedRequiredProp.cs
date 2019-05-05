using CCP.Attributes;

namespace CCP.UnitTests.Operations
{
    public class OperationWithAliasedRequiredProp : IOperation
    {
        [Alias(Name = "nr")]
        [Alias(Name = "nmbr")]
        [Required]
        public int Number { get; set; }

        public void Run()
        {
        }
    }
}