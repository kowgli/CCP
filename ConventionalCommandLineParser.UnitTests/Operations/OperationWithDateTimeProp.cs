﻿using System;

namespace CCP.UnitTests.Operations
{
    public class OperationWithDateTimeProp : IOperation
    {
        public DateTime Arg1 { get; set; }

        public void Run()
        {
        }
    }
}