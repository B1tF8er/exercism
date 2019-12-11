using System.Linq;
using static System.Math;
using static System.Convert;

public static class DifferenceOfSquares
{
    public static int CalculateSquareOfSum(int max) =>
        ToInt32(Pow(Enumerable.Range(1, max).Sum(), 2));

    public static int CalculateSumOfSquares(int max) =>
        Enumerable.Range(1, max).Select(n => ToInt32(Pow(n, 2))).Sum();

    public static int CalculateDifferenceOfSquares(int max) =>
        CalculateSquareOfSum(max) - CalculateSumOfSquares(max);
}