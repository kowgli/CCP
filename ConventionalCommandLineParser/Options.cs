using System.Globalization;

namespace CCP
{
    public class Options
    {
        public CultureInfo Locale { get; set; } = new CultureInfo("en-US");

        public string? DateFormat { get; set; } = null;

        public char ArrayElementSeparator { get; set; } = ';';

        internal static Options Default => new Options(); 
    }
}