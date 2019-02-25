namespace ConventionalCommandLineParser.Models
{
    internal class Argument
    {
        public Argument(string argument)
        {
            this.Name = "";
            this.Value = "";
        }

        public string Name { get; set; }
        public string Value { get; set; }
        public bool IsJson => Value?.StartsWith("{") ?? false;
        public bool IsLiteralString => Value?.StartsWith("\"") ?? false;
    }
}