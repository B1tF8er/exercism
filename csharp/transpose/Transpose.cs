using System.Linq;

public static class Transpose
{
    public static string String(string input)
    {
        var rows = input.Split('\n');
        var maxLineLength = rows.Max(row => row.Length);
        var transposed = new string[maxLineLength];

        for (var row = 0; row < rows.Length; row++)
        {
            for (var column = 0; column < rows[row].Length; column++)
                transposed[column] += rows[row][column];

            var remainderRowsMaximumLength = rows.Skip(row).Max(r => r.Length);
            for (var column = rows[row].Length; column < remainderRowsMaximumLength; column++)
                transposed[column] += " ";
        }
        
        return string.Join("\n", transposed).TrimEnd();
    }
}