enum OutputFormat
{
    ShortTime,
    ShortDateTime
}

static class OutputFormatExtensions
{
    public static string Apply(this OutputFormat format, DateTime time)
    {
        DateTimeOffset offset = time;
        switch(format)
        {
            case OutputFormat.ShortTime:
            return $"<t:{offset.ToUnixTimeSeconds()}:t>";
            case OutputFormat.ShortDateTime:
            return $"<t:{offset.ToUnixTimeSeconds()}:f>";
            default:
            throw new NotSupportedException("Invalid OutputFormat");
        }
    }
}