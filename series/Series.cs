using System;
using System.Linq;

public static class Series
{
    public static string[] Slices(string numbers, int sliceLength)
    {
        if (sliceLength < 1 || sliceLength > numbers.Length)
            throw new ArgumentException();

        return numbers
            .Where(IsSliceable(numbers, sliceLength))
            .Select(ToSlice(numbers, sliceLength))
            .ToArray();
    }

    private static Func<char, int, bool> IsSliceable(string numbers, int sliceLength) =>
        (_, index) => numbers.Substring(index).Length >= sliceLength;

    private static Func<char, int, string> ToSlice(string numbers, int sliceLength) =>
        (_, index) => numbers.Substring(index, sliceLength);
}