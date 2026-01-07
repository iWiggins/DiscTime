Cmd cmd = new();
var result = cmd.Parse(args);
if(result is null)
{
    return 1;
}
var dt = TimeRX.Match(result.Input);
if(dt is null)
{
    Console.Error.WriteLine($"Unable to parse input \"{result.Input}\"");
    return 2;
}
string output = result.Format.Apply((DateTime)dt);
Console.WriteLine(output);
return 0;