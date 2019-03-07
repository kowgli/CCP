using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCP.Exceptions
{
    public class NoOperationsException : Exception
    {
        public override string Message => "Command doesn't have any operations.";
    }
}
