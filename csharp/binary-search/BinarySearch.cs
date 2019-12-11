using System;

public static class BinarySearch
{
    public static int Find(int[] input, int value) =>
        input.Length == 0
            ? -1
            : SearchIndex(input, value, 0, input.Length - 1);

    private static int SearchIndex(int[] input, int value, int minIndex, int maxIndex)
    {
        var halveIndex = (minIndex + maxIndex) / 2;

        return input[halveIndex] == value
            ? halveIndex
            : halveIndex <= 0 || halveIndex >= maxIndex || minIndex == maxIndex
            ? -1
            : input[halveIndex] > value
            ? SearchIndex(input, value, minIndex, halveIndex - 1)
            : SearchIndex(input, value, halveIndex + 1, maxIndex);
    }
}