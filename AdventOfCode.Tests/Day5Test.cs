using System.Collections.Immutable;

namespace AdventOfCode.Tests;

public class Day5Tests
{
    [Fact]
    public void IntegrationTest()
    {
        var result = Day5.Run();

        result.MiddlePageNumbersSum.Should().Be(4957);
        result.FixedMiddlePageNumbersSum.Should().Be(6938);
    }

    [Fact]
    public void MiddlePageNumbersSumTest()
    {
        Day5.MiddlePageNumbersSum(Pages[..3]).Should().Be(143);
    }

    [Fact]
    public void ParseTest()
    {
        var input = "47|53\r\n97|13\r\n97|61\r\n97|47\r\n75|29\r\n61|13\r\n75|53\r\n29|13\r\n97|29\r\n53|29\r\n61|53\r\n97|53\r\n61|29\r\n47|13\r\n75|47\r\n97|75\r\n47|61\r\n75|61\r\n47|29\r\n75|13\r\n53|13\r\n\r\n75,47,61,53,29\r\n97,61,53,29,13\r\n75,29,13\r\n75,97,47,61,53\r\n61,13,29\r\n97,13,75,29,47";

        Day5.Parse(input).Should().BeEquivalentTo(new Day5.Data(
            Rules: Rules,
            Pages: Pages)
            );
    }
    private static readonly ImmutableArray<int[]> Pages = [
        [75, 47, 61, 53, 29], //0
        [97, 61, 53, 29, 13], //1
        [75, 29, 13], // 2
        [75, 97, 47, 61, 53], //3
        [61, 13, 29], //4
        [97, 13, 75, 29, 47]]; //5
    private static readonly ImmutableArray<(int, int)> Rules = [(47, 53), (97, 13), (97, 61), (97, 47), (75, 29), (61, 13), (75, 53), (29, 13), (97, 29), (53, 29), (61, 53), (97, 53), (61, 29), (47, 13), (75, 47), (97, 75), (47, 61), (75, 61), (47, 29), (75, 13), (53, 13)];

    [Theory]
    [InlineData(true, 0)]
    [InlineData(true, 1)]
    [InlineData(true, 2)]
    [InlineData(false, 3)]
    [InlineData(false, 4)]
    [InlineData(false, 5)]
    public void IsSortedTest(bool result, int pageIndex)
    {
        Day5.IsSorted(Pages[pageIndex], Rules).Should().Be(result);
    }

    [Theory]
    [InlineData(3, 97, 75, 47, 61, 53)]
    [InlineData(4, 61, 29, 13)]
    [InlineData(5, 97, 75, 47, 29, 13)]

    public void FixPageTest(int pageIndex, params int[] expectation)
    {
        var result = Day5.FixPage(Pages[pageIndex], Rules);
        result.Should()
            .BeEquivalentTo(expectation, o=>o.WithStrictOrdering());
    }

}
