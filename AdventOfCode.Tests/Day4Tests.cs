namespace AdventOfCode.Tests;

public class Day4Tests
{
    [Fact]
    public void IntegrationTest()
    {
        var result = Day4.Run();
        result.XmasCount.Should().Be(2521);
        result.MasXCount.Should().Be(1912);
    }

    [Fact]
    public void ColumnsSquareTest()
    {
        var input = new LetterGrid([
            "012",
            "345",
            "678",
        ]);
        input.Columns().Should().BeEquivalentTo([
            "036",
            "147",
            "258"]);
    }

    [Fact]
    public void ColumnsRectangleTest()
    {
        var input = new LetterGrid([
            "01",
            "34",
            "67",
        ]);

        input.Columns().Should().BeEquivalentTo([
            "036",
            "147"]);
    }

    [Fact]
    public void ForwardDiagnalTest()
    {
        var input = new LetterGrid([
            "012", 
            "345",
            "678",
        ]);

        input.ForwardDiagnals().Should().BeEquivalentTo([
            "0",
            "31",
            "642",
            "75",
            "8"]);
    }

    [Fact]
    public void ForwardDiagnalRectangleTest()
    {
        var input = new LetterGrid([
            "01",
            "34",
            "67",
        ]);

        input.ForwardDiagnals().Should().BeEquivalentTo([
            "0",
            "31",
            "64",
            "7"]);
    }

    [Fact]
    public void BackDiagnalTest()
    {
        var input = new LetterGrid([
            "012",
            "345",
            "678",
        ]);

        input.BackDiagnals().Should().BeEquivalentTo([
            "2",
            "51",
            "840",
            "73",
            "6"]);
    }

    [Fact]
    public void CountSmallXMasTest()
    {
        var input = new LetterGrid([
            "M.S",
            ".A.",
            "M.S"]);
        Day4.CountMasX(input).Should()
            .Be(1);
    }

    [Fact]
    public void CountXMasTest()
    {
        var input = new LetterGrid([
            ".M.S......",
            "..A..MSMS.",
            ".M.S.MAA..",
            "..A.ASMSM.",
            ".M.S.M....",
            "..........",
            "S.S.S.S.S.",
            ".A.A.A.A..",
            "M.M.M.M.M.",
            ".........."]);
        Day4.CountMasX(input).Should()
            .Be(9);
    }

    [Fact]
    public void CountSmallXmasTest()
    {
        var input = new LetterGrid([
            "..X...",
            ".SAMX.",
            ".A..A.",
            "XMAS.S",
            ".X...."]);

        Day4.CountXmas(input).Should()
            .Be(4);
    }

    [Fact]
    public void CountXmasTest()
    {
        var input = new LetterGrid([
            "MMMSXXMASM",
            "MSAMXMSMSA",
            "AMXSXMAAMM",
            "MSAMASMSMX",
            "XMASAMXAMM",
            "XXAMMXXAMA",
            "SMSMSASXSS",
            "SAXAMASAAA",
            "MAMMMXMMMM",
            "MXMXAXMASX"]);

        Day4.CountXmas(input).Should()
            .Be(18);
    }

}
