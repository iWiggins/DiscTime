using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

internal static partial class TimeRX
{
    [GeneratedRegex("""^\s*(?<hour>\d{1,2})\s*:\s*(?<minute>\d{1,2})\s*(?<meridiem>AM|PM)?\s*$""", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex ReShortTime();

    [GeneratedRegex("""^\s*(?<month>\d{1,2})\s*/\s*(?<day>\d{1,2})\s*/\s*(?<year>(\d{2}){1,2})\s*$""", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex ReShortDate();

    [GeneratedRegex("""^\s*(?<month>\d{1,2})\s*/\s*(?<day>\d{1,2})\s*/\s*(?<year>(\d{2}){1,2})\s+(?<hour>\d{1,2})\s*:\s*(?<minute>\d{1,2})\s*(?<meridiem>AM|PM)?\s*$""", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex ReShortDateShortTime();

    public static DateTime? Match(string input)
    {
        // attempt to match regular expressions from most complex to least.
        {
            var match = ReShortDateShortTime().Match(input);
            if(match.Success)
            {
                string dayS = match.Groups["day"].Value;
                string monthS = match.Groups["month"].Value;
                string yearS = match.Groups["year"].Value;
                string hourS = match.Groups["hour"].Value;
                string minuteS = match.Groups["minute"].Value;
                if(yearS.Length == 2) yearS = "20" + yearS;
                int day = int.Parse(dayS);
                int month = int.Parse(monthS);
                int year = int.Parse(yearS);
                int hour = int.Parse(hourS);
                int minute = int.Parse(minuteS);
                int offset = match.Groups["meridiem"].Value == "PM" ? 12 : 0;
                return new DateTime(year, month, day, hour + offset, minute, 0);
            }
        }
        {
            var match = ReShortDate().Match(input);
            if(match.Success)
            {
                string dayS = match.Groups["day"].Value;
                string monthS = match.Groups["month"].Value;
                string yearS = match.Groups["year"].Value;
                if(yearS.Length == 2) yearS = "20" + yearS;
                int day = int.Parse(dayS);
                int month = int.Parse(monthS);
                int year = int.Parse(yearS);
                return new DateTime(year, month, day);
            }
        }
        {
            var match = ReShortTime().Match(input);
            if(match.Success)
            {
                string hourS = match.Groups["hour"].Value;
                string minuteS = match.Groups["minute"].Value;
                int hour = int.Parse(hourS);
                int minute = int.Parse(minuteS);
                int offset = match.Groups["meridiem"].Value == "PM" ? 12 : 0;
                return DateTime.Now.Date + new TimeSpan(hour + offset, minute, 0);
            }
        }

        return null;
    }
}