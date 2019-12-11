using System;
using System.Linq;

public class Matrix
{
    private readonly int[][] rows;
    private readonly int[][] columns;

    public Matrix(string input)
    {
        rows = ParseRows(input);
        columns = ParseCols(rows);
    }

    public int Rows => rows.Length;
    public int Cols => columns.Length;

    public int[] Row(int row) => rows[row - 1];

    public int[] Column(int col) => columns[col - 1];

    private int[][] ParseRows(string input) =>
        input.Split('\n')
            .Select(ParseRow)
            .ToArray();

    private int[] ParseRow(string row) =>
        row.Split(' ')
            .Select(int.Parse)
            .ToArray();

    private int[][] ParseCols(int[][] rows) =>
        Enumerable.Range(0, rows[0].Length)
            .Select(y => ParseCol(rows, y))
            .ToArray();

    private int[] ParseCol(int[][] rows, int y) =>
        Enumerable.Range(0, rows.Length)
            .Select(x => rows[x][y])
            .ToArray();
}