using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConventionalCommandLineParser.Models
{
    public class Action
    {
        public string Name { get; internal set; }

        public Argument[] Arguments { get; internal set; }
    }
}
