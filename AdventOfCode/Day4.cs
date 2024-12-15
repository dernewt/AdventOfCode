
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode;

public static partial class Day4
{
    public record Results(int XmasCount, int MasXCount);
    public static Results Run()
    {
        var grid = new LetterGrid(File.ReadAllLines("Day4.cs.txt"));

        return new(CountXmas(grid), CountMasX(grid));
    }


    public static int CountXmas(LetterGrid grid)
    {
        var horizontalCount = grid.Rows()
            .Sum(text => ReversableOverlappingXmas().Matches(text).Count);

        var verticalCount = grid.Columns()
            .Sum(text => ReversableOverlappingXmas().Matches(text).Count);

        var diagCount = grid.ForwardDiagnals()
            .Sum(text => ReversableOverlappingXmas().Matches(text).Count);

        var backDiagCount = grid.BackDiagnals()
            .Sum(text => ReversableOverlappingXmas().Matches(text).Count);

        return horizontalCount + verticalCount + diagCount + backDiagCount;
    }

    public static int CountMasX(LetterGrid grid)
    {
        var count = 0;

        foreach (var subgrid in grid.SubGrids33())
        {
            // GOTCHA below is overkill for the subgrid approach
            // there is a more elogant way on the whole grid at once
            // but I just did this special case of 3x3 grids
            var forwardDiagMatches = subgrid.ForwardDiagnals()
                .Select(text => ReversableOverlappingMas().Matches(text)
                    .Select(m => m.Index).ToArray());

            var backDiagMatches = subgrid.BackDiagnals()
                .Select(text => ReversableOverlappingMas().Matches(text)
                    .Select(m => m.Index).ToArray());

            count += forwardDiagMatches.SelectMany(m=>m)
                .Intersect(backDiagMatches.SelectMany(m=>m))
                .Count();
        }
        return count;
    }

    [GeneratedRegex("(?<=X)MAS|(?<=S)AMX")]
    private static partial Regex ReversableOverlappingXmas();

    [GeneratedRegex("(?<=M)AS|(?<=S)AM")]
    private static partial Regex ReversableOverlappingMas();
}

public class LetterGrid(string[] grid)
{
    public IEnumerable<string> Rows() => grid;
    public IEnumerable<string> Columns()
    {
        for (var rowIndex = 0; rowIndex < grid.First().Length; rowIndex++)
        {
            var vertical = new char[grid.Length];
            for (var columnIndex = 0; columnIndex < grid.Length; columnIndex++)
            {
                vertical[columnIndex] = grid[columnIndex][rowIndex];
            }
            yield return new(vertical);
        }
    }

    public IEnumerable<string> BackDiagnals()
    {
        for (var (row, col) = (0, grid[0].Length - 1); row < grid.Length && col >= 0;)
        {
            yield return BackDiagnalFromBottom(row, col);

            if (row == grid.Length - 1)
                col--;
            else
                row++;
        }
    }

    public IEnumerable<string> ForwardDiagnals()
    {
        for (var (row, col) = (0, 0); row < grid.Length && col < grid[row].Length;)
        {
            yield return ForwardDiagnalFromBottom(row, col);

            if (row == grid.Length - 1)
                col++;
            else
                row++;
        }
    }

    public string BackDiagnalFromBottom(int startRow, int startCol)
    {
        var result = new StringBuilder();
        for (var (row, col) = (startRow, startCol); row >= 0 && col >= 0;)
        {
            result.Append(grid[row--][col--]);
        }
        return result.ToString();
    }
    public string ForwardDiagnalFromBottom(int startRow, int startCol)
    {
        var result = new StringBuilder();
        for (var (row, col) = (startRow, startCol); row >= 0 && col < grid[row].Length;)
        {
            result.Append(grid[row--][col++]);
        }
        return result.ToString();
    }

    public IEnumerable<LetterGrid> SubGrids33()
    {
        var width = 3;
        var height = 3;

        for (var row = 0; row < grid.Length && row + height <= grid.Length; row++)
        {
            for (var col = 0; col < grid[row].Length && col + width <= grid[row].Length; col++)
            {
                yield return new(
                    grid.Skip(row).Take(height)
                    .Select(r => r[col..(col+width)])
                    .ToArray());
            }
        }
    }
}
