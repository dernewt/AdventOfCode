
using System.Text.RegularExpressions;

namespace AdventOfCode;

public class Day3
{
    public static Results Run()
    {
        var commands = ParseCommands(File.ReadAllText("Day3.cs.txt"));

        //part 1
        var multiplicationResults = ExecuteMultipications(commands);

        //part 2
        var multiplicationConditionalResults = ExecuteEnabledMultipications(commands);

        return new(multiplicationResults.Sum(), multiplicationConditionalResults.Sum());
    }

    const string OpDo = "do";
    const string OpDont = "don't";
    const string OpMul = "mul";
    public record Command(string Name, int? Arg1 = null, int? Arg2 = null);

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance",
        "SYSLIB1045:Convert to 'GeneratedRegexAttribute'.",
        Justification = "Readability over performance, may be more dynamic in the future.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality",
        "IDE0079:Remove unnecessary suppression.",
        Justification = "IDE BUG SYSLIB1045 is seen as IDE0079 even though it's not.")]
    public static IEnumerable<Command> ParseCommands(string input)
    {
        var matches = Regex.Matches(input,
            "(?<op>" + OpMul + @")\((?<arg1>\d{1,3}),(?<arg2>\d{1,3})\)" +
            "|(?<op>" + OpDo + @")\(\)" +
            "|(?<op>" + OpDont + @")\(\)");

        var commands = matches.Select(m => new Command(
            m.Groups["op"].Value,
            m.Groups["arg1"].Success ? int.Parse(m.Groups["arg1"].Value) : null,
            m.Groups["arg2"].Success ? int.Parse(m.Groups["arg2"].Value) : null
            ));

        return commands;
    }

    public static IEnumerable<int> ExecuteMultipications(IEnumerable<Command> commands)
    {
        return commands
            .Where(p => p.Name == OpMul)
            .Select(p => p.Arg1!.Value * p.Arg2!.Value);
    }

    public static IEnumerable<int> ExecuteEnabledMultipications(IEnumerable<Command> commands)
    {
        var isEnabled = true;
        var filtered = commands.Where(cmd =>
        {
            isEnabled = cmd.Name switch
            {
                OpDont => false,
                OpDo => true,
                _ => isEnabled
            };
            return isEnabled;
        });

        return ExecuteMultipications(filtered);
    }
}
