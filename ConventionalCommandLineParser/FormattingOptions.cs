using System.Globalization;

namespace ConventionalCommandLineParser
{
    public class FormattingOptions
    {
        public CultureInfo Locale { get; set; } = new CultureInfo("en-US");

        public string? DateFormat { get; set; } = null;

        internal static FormattingOptions Default => new FormattingOptions();        
    }
}