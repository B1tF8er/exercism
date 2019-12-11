using System;
using System.Linq;

public enum Classification
{
    Perfect,
    Abundant,
    Deficient
}

public static class PerfectNumbers
{
    public static Classification Classify(int number)
    {
        if (number < 1)
            throw new ArgumentOutOfRangeException(nameof(number));

        var aliquotSum = number.GetAliquotSum();

        if (aliquotSum == number)
            return Classification.Perfect;

        if (aliquotSum > number)
            return Classification.Abundant;

        return Classification.Deficient;
    }

    private static int GetAliquotSum(this int number) =>
        Enumerable
            .Range(1, number - 1)
            .Where(n => number % n == 0)
            .Sum();
}
