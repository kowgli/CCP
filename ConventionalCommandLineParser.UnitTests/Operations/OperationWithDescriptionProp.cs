using CCP.Attributes;

namespace CCP.UnitTests.Operations
{
    [Description("Operation description")]
    public class OperationWithDescriptionProp : IOperation
    {
        public string Arg1 { get; set; }

        [Description("Test description")]
        public string Arg2 { get; set; }

        [Description(Description = "Test description 2")]
        public string Arg3 { get; set; }

        public void Run()
        {
        }
    }
}