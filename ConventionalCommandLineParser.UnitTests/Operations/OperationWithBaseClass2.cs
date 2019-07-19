namespace CCP.UnitTests.Operations
{
    public class OperationWithBaseClass2 : AbstractBaseOperation, IOperation
    {
        public int Arg3 { get; set; }

        public void Run()
        {
            throw new System.NotImplementedException();
        }
    }
}
