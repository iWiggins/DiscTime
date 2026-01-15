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
        return format switch
        {
            OutputFormat.ShortTime => $"<t:{offset.ToUnixTimeSeconds()}:t>",
            OutputFormat.ShortDateTime => $"<t:{offset.ToUnixTimeSeconds()}:f>",
            _ => throw new NotSupportedException("Invalid OutputFormat"),
        };
    }
}