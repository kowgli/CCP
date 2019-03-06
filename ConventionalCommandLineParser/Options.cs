using System.Globalization;

namespace CCP
{
    public class Options
    {
        public CultureInfo Locale { get; set; } = new CultureInfo("en-US");

        public string? DateFormat { get; set; } = null;

        internal static Options Default => new Options(); 
    }
}