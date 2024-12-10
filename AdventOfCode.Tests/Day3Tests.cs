namespace AdventOfCode.Tests;

public class Day3Tests
{
    [Fact]
    public void IntegrationTest()
    {
        var result = Day3.Run();
        result.MultiplicationsSum.Should().Be(174960292);

        result.ConditionalMultiplicationsSum.Should().Be(56275602);
    }

    [Fact]
    public void ParseMulTest()
    {
        var input = @"xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))";
        var commands = Day3.ParseCommands(input);
        commands.Should().BeEquivalentTo<Day3.Command>([new("mul", 2, 4), new("mul", 5, 5), new("mul", 11, 8), new("mul", 8, 5)]);
    }

    [Fact]
    public void ParseMulDoDontTest()
    {
        var input = @"xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))";
        var commands = Day3.ParseCommands(input);
        commands.Should().BeEquivalentTo<Day3.Command>([new("mul", 2, 4), new("don't"), new("mul", 5, 5), new("mul", 11, 8), new("do"), new("mul", 8, 5)]);
    }

    [Fact]
    public void ExecuteMultipicationsTest()
    {
        var results = Day3.ExecuteMultipications([
            new("mul", 0, 0),
            new("poo", 3, 3), //skip
            new("mul", 2, 2),
        ]);
        results.Should().BeEquivalentTo([
            0,
            4,
        ]);
    }

    [Fact]
    public void ExecuteEnabledMultipicationsTest()
    {
        var results = Day3.ExecuteEnabledMultipications([
            new("mul", 2, 4), //yes
            new("don't", 0, 0), //disable
            new("mul", 5, 5), //no
            new("mul", 11, 8), //no
            new("do", 0, 0), //enable
            new("mul", 8, 5) //yes
        ]);
        results.Should().BeEquivalentTo([
            8,
            40,
        ]);
    }

}
