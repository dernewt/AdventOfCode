global using FluentAssertions;

namespace AdventOfCode.Tests;

public class Day1Tests
{
    [Fact]
    public void ParseTest()
    {
        var input = "3   4\r\n4   3\r\n2   5\r\n1   3\r\n3   9\r\n3   3";
        var result = Day1.Parse(input);
        result.Item1.Should().BeEquivalentTo([3, 4, 2, 1, 3, 3]);
        result.Item2.Should().BeEquivalentTo([4, 3, 5, 3, 9, 3]);
    }

    [Fact]
    public void CalcTotalDifferenceTest()
    {
        var totalDifference = Day1.CalcTotalDifference(
            [3, 4, 2, 1, 3, 3],
            [4, 3, 5, 3, 9, 3]);

        totalDifference.Should().Be(11);
    }

    [Fact]
    public void CalcSimilarityScore()
    {
        var similarityScore = Day1.CalcSimilarityScore(
            [3, 4, 2, 1, 3, 3],
            [4, 3, 5, 3, 9, 3]);

        similarityScore.Should().Be(31);
    }

    [Fact]
    public void Integration()
    {
        var result = Day1.Run();
        result.TotalDifference.Should().Be(3714264);
        result.SimilarityScore.Should().Be(18805872);
    }
}
