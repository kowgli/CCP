using ConventionalCommandLineParser.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConventionalCommandLineParser.UnitTests.Executors
{
    public class CommandWithRequiredProp : IExecutable
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
