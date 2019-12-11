using System;
using System.Linq;

public enum Direction
{
    North,
    East,
    South,
    West
}

public class RobotSimulator
{
    private const string InvalidInstruction = "Invalid instruction";
    private const string InvalidDirection = "Invalid direction";

    public RobotSimulator(Direction direction, int x, int y)
    {
        Direction = direction;
        X = x;
        Y = y;
    }

    public Direction Direction { get; private set; }

    public int X { get; private set; }

    public int Y { get; private set; }

    public void Move(string instructions) =>
        instructions.ToList().ForEach(Move);
    
    private void Move(char step)
    {
        switch (step)
        {
            case 'L': Left(); break;
            case 'R': Right(); break;
            case 'A': Advance(); break;
            default: throw new InvalidOperationException(InvalidInstruction);
        }
    }

    private void Left()
    {
        switch (Direction)
        {
            case Direction.North: MoveToWest(); break;
            case Direction.East: MoveToNorth(); break;
            case Direction.South: MoveToEast(); break;
            case Direction.West: MoveToSouth(); break;
            default: throw new InvalidOperationException(InvalidDirection);
        }
    }

    private void Right()
    {
        switch (Direction)
        {
            case Direction.North: MoveToEast(); break;
            case Direction.East: MoveToSouth(); break;
            case Direction.South: MoveToWest(); break;
            case Direction.West: MoveToNorth(); break;
            default: throw new InvalidOperationException(InvalidDirection);
        }
    }

    private void Advance()
    {
        switch (Direction)
        {
            case Direction.North: Y++; break;
            case Direction.East: X++; break;
            case Direction.South: Y--; break;
            case Direction.West: X--; break;
        }
    }

    private void MoveToNorth() => Direction = Direction.North;

    private void MoveToEast() => Direction = Direction.East;
    
    private void MoveToSouth() => Direction = Direction.South;
    
    private void MoveToWest() => Direction = Direction.West;
}