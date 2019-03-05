using System.Collections.Generic;

namespace CCP.Models
{
    internal class Operation
    {
        private List<Argument> arguments = new List<Argument>();

        internal Operation(string name)
        {
            this.Name = name ?? "";
        }

        public string Name { get; }

        public Argument[] Arguments => arguments.ToArray();

        public void AddArgument(Argument argument) => arguments.Add(argument);      
    }
}