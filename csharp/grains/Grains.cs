using System;
using System.Linq;
using static System.Convert;

public static class Grains
{
    public static ulong Square(int n) =>
        (n <= 0 || n > 64)
            ? throw new ArgumentOutOfRangeException(nameof(n))
            : n == 1 ? 1 : 2 * Square(n - 1);

    public static ulong Total() =>
        ToUInt64(Enumerable
            .Range(1, 64)
            .Select(n => Square(n))
            .Select(ToDecimal)
            .Sum());
}