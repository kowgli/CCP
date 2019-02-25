using System.Collections.Generic;

namespace ConventionalCommandLineParser.Models
{
    internal class Command
    {
        private List<Argument> arguments = new List<Argument>();

        internal Command(string name)
        {
            this.Name = name ?? "";
        }

        public string Name { get; }

        public Argument[] Arguments => arguments.ToArray();

        public void AddArgument(Argument argument) => arguments.Add(argument);      
    }
}