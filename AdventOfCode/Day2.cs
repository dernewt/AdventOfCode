using System.Collections.Immutable;

namespace AdventOfCode;

public class Day2
{
    public static Results Run()
    {
        var levels = Parse(File.ReadAllText("Day2.cs.txt"))
            .ToImmutableArray();

        var safeLevels = levels.Where(IsSafe);

        var safeLevelsDampened = levels.Where(IsSafeDampened);

        return new(safeLevels.Count(), safeLevelsDampened.Count());

    }

    public static IEnumerable<int[]> Parse(string input)
    {
        var rowsWithints = input.Split(Environment.NewLine)
            .Select(row => row.Split(' ')
                           .Select(level => int.Parse(level))
                           .ToArray());

        return rowsWithints;
    }

    public static bool IsSafe(int[] levels)
    {
        if (levels.Length <= 1) return true;

        var direction = Math.Sign(levels[0] - levels[1]);
        for (int i = 0 + 1; i < levels.Length; i++)
        {
            var previous = levels[i - 1];
            var current = levels[i];

            if (Math.Sign(previous - current) != direction)
                return false;

            var levelDifference = Math.Abs(previous - current);
            if (levelDifference < 1 || levelDifference > 3)
                return false;
        }

        return true;
    }

    public static bool IsSafeDampened(int[] levels)
    {
        foreach (var index in Enumerable.Range(0, levels.Length))
        {
            if (IsSafe(levels.Where((_, i) => i != index).ToArray()))
                return true;
        }
        return false;
    }
}
