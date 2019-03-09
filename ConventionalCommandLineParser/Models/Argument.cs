using CCP.Exceptions;
using System.Text.RegularExpressions;

namespace CCP.Models
{
    internal class Argument
    {
        private static string arraySeparator = ";";

        public Argument(string argument)
        {           
            string[] split = (argument ?? "").Split('=');
            if(split.Length != 2)
            {
                throw new ArgumentFormatException(argument);
            }

            this.Name = split[0];
            this.Value = split[1];
            this.Values = SplitIntoArray(this.Value, arraySeparator);
        }

        public string Name { get; private set; }
        public string Value { get; private set; }
        public string[] Values { get; private set; }
        public bool IsPotentialJson => Value?.StartsWith("{") ?? false;
        public bool IsLiteralString => Value?.StartsWith("\"") ?? false;
        public bool IsArray { get; private set; }

        private string[] SplitIntoArray(string value, string separatorChar)
        {
            if(!value.Contains(arraySeparator))
            {
                return new[] { this.Value };
            }

            // Got help from here: https://stackoverflow.com/questions/3147836/c-sharp-regex-split-commas-outside-quotes
            // Thanks Jens!
            Regex splitter = new Regex(separatorChar + "(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
            return splitter.Split(value);
        }
    }
}