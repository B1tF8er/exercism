using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Coordinate = System.Tuple<int, int>;

public enum ConnectWinner
{
    White,
    Black,
    None
}

public class Connect
{
    private enum Cell
    {
        Empty,
        White,
        Black
    }

    private int Cols => board[0].Length;
    private int Rows => board.Length;

    private readonly Cell[][] board;

    public Connect(string[] input) =>
        board = ParseBoard(input);
    
    public ConnectWinner Result()
    {
        if (WhiteWins())
            return ConnectWinner.White;

        if (BlackWins())
            return ConnectWinner.Black;

        return ConnectWinner.None;
    }

    private static Cell CharToCell(char c)
    {
        switch (c)
        {
            case 'O': return Cell.White;
            case 'X': return Cell.Black;
            default: return Cell.Empty;
        }
    }

    private static Cell[][] ParseBoard(string[] input) =>
        input
            .Select(str => Regex.Replace(str, @"\s+", ""))
            .Select(row => row.Select(CharToCell).ToArray()).ToArray();

    private bool IsValidCoordinate(Coordinate coordinate) =>
        coordinate.Item2 >= 0 && coordinate.Item2 < Rows &&
        coordinate.Item1 >= 0 && coordinate.Item1 < Cols;

    private bool CellAtCoordinateEquals(Cell cell, Coordinate coordinate) =>
        board[coordinate.Item2][coordinate.Item1] == cell;

    private HashSet<Coordinate> Adjacent(Cell cell, Coordinate coordinate)
    {
        var row = coordinate.Item2;
        var col = coordinate.Item1;

        var coordinates = new[]
        {
            new Coordinate(col + 1, row - 1),
            new Coordinate(col,     row - 1),
            new Coordinate(col - 1, row    ),
            new Coordinate(col + 1, row    ),
            new Coordinate(col - 1, row + 1),
            new Coordinate(col,     row + 1)
        };

        return new HashSet<Coordinate>(
            coordinates
                .Where(coord => IsValidCoordinate(coord) && CellAtCoordinateEquals(cell, coord))
        );
    }
    
    private bool ValidPath(Cell cell, Func<Cell[][], Coordinate, bool> stop, HashSet<Coordinate> processed, Coordinate coordinate)
    {
        if (stop(board, coordinate))
            return true;

        var next = Adjacent(cell, coordinate);
        next.ExceptWith(processed);
        
        if (!next.Any())
            return false;

        return next.Any(nextCoord => {
            var updatedProcessed = new HashSet<Coordinate>(processed);
            updatedProcessed.Add(nextCoord);

            return ValidPath(cell, stop, updatedProcessed, nextCoord);
        });
    }   

    private bool IsWhiteStop(Cell[][] board, Coordinate coordinate) =>
        coordinate.Item2 == Rows - 1;

    private bool IsBlackStop(Cell[][] board, Coordinate coordinate) =>
        coordinate.Item1 == Cols - 1;

    private HashSet<Coordinate> WhiteStart() =>
        new HashSet<Coordinate>(
            Enumerable.Range(0, Cols)
                .Select(col => new Coordinate(col, 0))
                .Where(coord => CellAtCoordinateEquals(Cell.White, coord))
        );
        
    private HashSet<Coordinate> BlackStart() =>
        new HashSet<Coordinate>(
            Enumerable.Range(0, Rows)
                .Select(row => new Coordinate(0, row))
                .Where(coord => CellAtCoordinateEquals(Cell.Black, coord))
        );

    private bool ColorWins(Cell cell, Func<Cell[][], Coordinate, bool> stop, Func<HashSet<Coordinate>> start) =>
        start()
            .Any(coordinate => 
                ValidPath(cell, stop, new HashSet<Coordinate>(), coordinate)
            );

    private bool WhiteWins() =>
        ColorWins(Cell.White, IsWhiteStop, WhiteStart);

    private bool BlackWins() =>
        ColorWins(Cell.Black, IsBlackStop, BlackStart);  
}