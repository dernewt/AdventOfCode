namespace AdventOfCode.Tests;

public class Day2Tests
{
    [Fact]
    public void ParseTest()
    {
        var input = "7 6 4 2 1\r\n1 2 7 8 9\r\n9 7 6 2 1\r\n1 3 2 4 5\r\n8 6 4 4 1\r\n1 3 6 7 9";
        var result = Day2.Parse(input);
        result.First().Should().BeEquivalentTo([7,6,4,2,1]);
        result.Count().Should().Be(6);
        result.Last().Should().BeEquivalentTo([1, 3, 6, 7, 9]);
    }

    [Theory]
    [InlineData(true, "single or less is safe", 42)]
    [InlineData(true, "both increasing", 42, 43)]
    [InlineData(true, "all decreasing by 1 or 2", 7, 6, 4, 2, 1)]
    [InlineData(false, "2 7 is an increase of 5", 1, 2, 7, 8, 9)]
    [InlineData(false, "6 2 is a decrease of 4", 9, 7, 6, 2, 1)]
    [InlineData(false, "1 3 is increasing but 3 2 is decreasing", 1, 3, 2, 4, 5)]
    [InlineData(false, "4 4 is neither an increase or a decrease", 8, 6, 4, 4, 1)]
    [InlineData(true, "all increasing by 1, 2, or 3", 1, 3, 6, 7, 9)]
    public void SafeTest(bool result, string because, params int[] levels)
    {
        Day2.IsSafe(levels).Should().Be(result, because);
    }

    [Theory]
    [InlineData(true, "all decreasing by 1 or 2", 7, 6, 4, 2, 1)]
    [InlineData(true, "all increasing by 1, 2, or 3", 1, 3, 6, 7, 9)]
    [InlineData(true, "remove 9", 9, 1, 3, 6, 7, 9)]
    [InlineData(true, "remove 71", 71, 69, 70, 71, 72, 75)]
    [InlineData(true, "remove 86", 83, 86, 81, 78, 77, 75)]
    [InlineData(true, "remove 3", 1, 3, 2, 4, 5)]
    [InlineData(true, "remove 4", 8, 6, 4, 4, 1)]
    [InlineData(false, "2 7 is an increase of 5 and 2 8 is worse", 1, 2, 7, 8, 9)]
    [InlineData(false, "6 2 is a decrease of 4 and 7 2 is worse", 9, 7, 6, 2, 1)]
    public void SafeDamperTest(bool result, string because, params int[] levels)
    {
        Day2.IsSafeDampened(levels).Should().Be(result, because);
    }

    [Fact]
    public void Integration()
    {
        var result = Day2.Run();
        result.SafeLevels.Should().Be(534);
        result.SafeLevelsDampened.Should().Be(577);
    }
}
