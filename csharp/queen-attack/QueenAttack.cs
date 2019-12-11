using System;
using System.Collections.Generic;
using System.Linq;

public class Queen
{
    private const int MinPosition = 0;
    private const int MaxPosition = 7;

    private Queen(int row, int column)
    {
        Row = row;
        Column = column;
    }

    internal static Queen CreateAt(int row, int column)
    {
        Guard(row);
        Guard(column);

        return new Queen(row, column);
    }

    private static void Guard(int position)
    {
        if (position < MinPosition || position > MaxPosition)
            throw new ArgumentOutOfRangeException(nameof(position));
    }

    public int Row { get; }
    public int Column { get; }

    internal bool CanAttack(Queen other) =>
        SameRow(other) || SameColumn(other) || InRange(other);

    private bool SameRow(Queen other) =>
        Row == other.Row;

    private bool SameColumn(Queen other) =>
        Column == other.Column;

    private bool InRange(Queen other) =>
        Math.Abs(Row - other.Row) == Math.Abs(Column - other.Column);
}

public static class QueenAttack
{
    public static bool CanAttack(Queen white, Queen black) =>
        white.CanAttack(black);

    public static Queen Create(int row, int column) =>
        Queen.CreateAt(row, column);
}