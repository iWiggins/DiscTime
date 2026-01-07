
static class Util
{
    public static IEnumerable<string> PermuteCase(string str)
    {
        yield return str;
        yield return str.ToLower();
        yield return str.ToUpper();
        yield return str.Replace(str[0], char.ToUpper(str[0]));
    }
}