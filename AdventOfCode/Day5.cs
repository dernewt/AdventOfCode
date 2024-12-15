
using System.Text.RegularExpressions;

namespace AdventOfCode;

public partial class Day5
{
    public record Results(int MiddlePageNumbersSum, int FixedMiddlePageNumbersSum);
    public static Results Run()
    {
        var data = Parse(File.ReadAllText("Day5.cs.txt"));

        var sortedPages = data.Pages.ToLookup(p => IsSorted(p, data.Rules));

        return new(
            MiddlePageNumbersSum(sortedPages[true]),
            MiddlePageNumbersSum(sortedPages[false].Select(p => FixPage(p, data.Rules))));
    }

    public class RuleOrder(IEnumerable<(int, int)> Rules) : IComparer<int>
    {
        public int Compare(int x, int y)
            => Rules.Contains((y, x)) ? 1 : -1;
    }

    public static int[] FixPage(IEnumerable<int> pageNumbers, IEnumerable<(int, int)> rules)
    {
        var sortedPageNumbers = pageNumbers.Order(new RuleOrder(rules));

        return [.. sortedPageNumbers];
    }

    public static int MiddlePageNumbersSum(IEnumerable<int[]> pages) => pages
        .Sum(page => page[page.Length / 2]);

    public record Data(IEnumerable<(int, int)> Rules, IEnumerable<int[]> Pages);

    public static Data Parse(string input)
    {
        var rules = new List<(int, int)>();
        var pages = new List<int[]>();
        foreach (Match match in RulesAndPages().Matches(input))
        {
            if (match.Groups["rule"].Success)
            {
                var rule = match.Groups["rule"].Captures
                    .Select(r => int.Parse(r.Value))
                    .ToArray();
                rules.Add((rule[0], rule[1]));
            }
            else if (match.Groups["page"].Success)
            {
                var page = match.Groups["page"].Captures
                    .Select(r => int.Parse(r.Value))
                    .ToArray();
                pages.Add(page);
            }
            //else throw it away
        }

        return new(rules, pages);
    }

    [GeneratedRegex(@"(?<rule>\d+)\|(?<rule>\d+)|(?:(?<page>\d+),)+(?<page>\d+)")]
    private static partial Regex RulesAndPages();

    public static bool IsSorted(int[] pages, IEnumerable<(int, int)> rules)
    {
        var pageLookup = pages.Index()
            .ToDictionary(p => p.Item, p => p.Index);
        foreach (var rule in rules)
        {
            if (pageLookup.TryGetValue(rule.Item1, out var location1)
                && pageLookup.TryGetValue(rule.Item2, out var location2)
                && location1 > location2)
                return false;
        }
        return true;
    }
}
