using System;

namespace CCP.UnitTests.Executors
{
    public class OperationWithSimpleProps : IOperation
    {
        public string Arg1 { get; set; }

        public int Arg2 { get; set; }

        public decimal Arg3 { get; set; }

        public decimal Arg4 { get; private set; }

#pragma warning disable IDE0052 // Remove unread private members
        private decimal Arg5 { get;  set; }
#pragma warning restore IDE0052 // Remove unread private members

        public void Run()
        {
            Arg5 = 0;
        }
    }
}