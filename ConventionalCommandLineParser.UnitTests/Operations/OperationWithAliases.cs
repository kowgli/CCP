using CCP.Attributes;

namespace CCP.UnitTests.Operations
{
    [Alias(Name = "a")]
    [Alias(Name = "some_alias")]
    public class OperationWithAliases : IOperation
    {
        public void Run()
        {
        }
    }
}