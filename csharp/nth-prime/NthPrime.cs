using System;
using System.Collections.Generic;
using System.Linq;

public static class NthPrime
{
    private const int One = 1;
    private const int MaxPrime = 104743;

    private static IEnumerable<(int, int)> primes;

    static NthPrime() =>
        primes = Enumerable
            .Range(One, MaxPrime)
            .Where(IsPrime)
            .Select((number, index) => (number, index + 1));

    public static int Prime(int nth) =>
        nth <= 0
            ? throw new ArgumentOutOfRangeException(nameof(nth))
            : primes.FirstOrDefault(prime => prime.Item2 == nth).Item1;

    private static bool IsPrime(int number)
    {
        if (number < 2) return false;
        if (number == 2) return true;
        if (number % 2 == 0) return false;

        for (int i = 3; i * i <= number; i += 2)
            if (number % i == 0) return false;

        return true;
    }
}