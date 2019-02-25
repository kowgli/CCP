using ConventionalCommandLineParser.Exceptions;

namespace ConventionalCommandLineParser.Models
{
    internal class Argument
    {
        public Argument(string argument)
        {           
            string[] split = (argument ?? "").Split('=');
            if(split.Length != 2)
            {
                throw new ArgumentFormatException();
            }

            this.Name = split[0];
            this.Value = split[1];
        }

        public string Name { get; set; }
        public string Value { get; set; }
        public bool IsPotentialJson => Value?.StartsWith("{") ?? false;
        public bool IsLiteralString => Value?.StartsWith("\"") ?? false;
    }
}