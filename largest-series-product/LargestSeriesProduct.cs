using System;
using System.Collections.Generic;
using System.Linq;

public static class LargestSeriesProduct
{
    public static long GetLargestProduct(string digits, int span) =>
        digits.AsLong()
            .GetSlicesFor(span)
            .Max(number => number.GetProduct());

    private static IEnumerable<long> AsLong(this string digits) =>
        digits.Select(ParseDigit);

    private static long ParseDigit(this char c)
    {
        if (!char.IsDigit(c))
            throw new ArgumentException("Invalid digit.");

        return long.Parse(c.ToString());
    }

    private static IEnumerable<IEnumerable<long>> GetSlicesFor(this IEnumerable<long> digits, int span)
    {
        if (span < 0 || span > digits.Count())
            throw new ArgumentException("Invalid span.");

        return Enumerable
            .Range(0, GetNumberOfSlices(digits, span))
            .Select(i => digits.Skip(i).Take(span));
    }

    private static int GetNumberOfSlices(this IEnumerable<long> digits, int span) =>
        digits.Count() + 1 - span;

    private static long GetProduct(this IEnumerable<long> numbers) =>
        numbers.Aggregate(1L, (x, product) => x * product);
}