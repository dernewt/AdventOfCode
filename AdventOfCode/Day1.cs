namespace AdventOfCode;

public class Day1
{
    public static Results Run()
    {
        var columns = Parse(File.ReadAllText("Day1.cs.txt"));

        //part 1
        var totalDifference = CalcTotalDifference(columns.Item1, columns.Item2);

        //part 2
        var simalarityScore = CalcSimilarityScore(columns.Item1, columns.Item2);

        return new(totalDifference, simalarityScore);
    }

    public static (int[], int[]) Parse(string input)
    {
        var rows = input
            .Split(Environment.NewLine)
            .Select(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries))
            .Select(row => (int.Parse(row[0]), int.Parse(row[1])))
            .Aggregate((new List<int>(), new List<int>() ), (root, row) =>
            {
                root.Item1.Add(row.Item1);
                root.Item2.Add(row.Item2);
                return root;
            });
        return ([.. rows.Item1], [.. rows.Item2]);
    }

    public static int CalcTotalDifference(IEnumerable<int> left, IEnumerable<int> right)
    {
        var totalDifference = left.Order()
            .Zip(right.Order(), (left, right) => Math.Abs(left - right))
            .Sum();
        return totalDifference;
    }

    public static int CalcSimilarityScore(IEnumerable<int> left, IEnumerable<int> right)
    {
        var rightLookup = right.ToLookup(num => num);
        var similarityScore = left.Sum(l => l * rightLookup[l].Count());
        return similarityScore;
    }
}
