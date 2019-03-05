using CCP.Attributes;

namespace CCP.UnitTests.Executors
{
    public class OperationWithRequiredProp : IOperation
    {
        public string Arg1 { get; set; }

        [Required]
        public string Arg2 { get; set; }

        public string Arg3 { get; set; }

        public void Run()
        {
        }
    }
}