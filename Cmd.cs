using System.CommandLine;

class Cmd
{
    private readonly Option<string> _optionFormat;
    private readonly Argument<string[]> _argumentTime;
    private readonly RootCommand _root;

    private static string[] OutputNames => [.. (
        from name in Enum.GetNames<OutputFormat>()
        from perm in Util.PermuteCase(name)
        select perm
        )];

    public record Result(string Input, OutputFormat Format);

    public Cmd()
    {
        _optionFormat = new("--format","-f")
        {
            Description = "The output format of the timestamp.",
            DefaultValueFactory = r => "shorttime"
        };
        _optionFormat.AcceptOnlyFromAmong(OutputNames);
        
        _argumentTime = new("time")
        {
            Description = "The time to convert.",
        };
        _root = new("A utility to generate Discord-compatible UTC timestamps.");
        _root.Options.Add(_optionFormat);
        _root.Arguments.Add(_argumentTime);
    }

    public Result? Parse(string[] args)
    {
        var parseResult = _root.Parse(args);
        if(parseResult.Errors.Any())
        {
            foreach(var err in parseResult.Errors)
            {
                Console.Error.WriteLine(err.Message);
            }
            return null;
        }
        else
        {
            string time = string.Join(' ', parseResult.GetValue(_argumentTime) ?? []);
            string? formatS = parseResult.GetValue(_optionFormat) ?? "shorttime";
            OutputFormat format = Enum.Parse<OutputFormat>(formatS, true);
            return new(time, format);
        }
    }
}