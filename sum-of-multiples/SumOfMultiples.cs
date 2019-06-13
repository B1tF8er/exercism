using System;
using System.Collections.Generic;
using System.Linq;

public static class SumOfMultiples
{
    public static int Sum(IEnumerable<int> multiples, int max) =>
        Enumerable.Range(1, max - 1)
            .Where(NumberIsMultipleOfAny(multiples))
            .Sum();

    private static Func<int, bool> NumberIsMultipleOfAny(IEnumerable<int> multiples) =>
         number => multiples
            .Where(multiple => multiple != 0)
            .Any(multiple => number % multiple == 0);
}