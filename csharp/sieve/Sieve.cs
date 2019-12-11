using System;
using System.Collections.Generic;
using System.Linq;

public static class Sieve
{
    public static int[] Primes(int limit) =>
        limit.GetPrimes().ToArray();

    private static IEnumerable<int> GetPrimes(this int limit)
    {
        if (limit < 2)
            throw new ArgumentOutOfRangeException(nameof(limit));

        var primes = Enumerable.Range(2, limit - 1);

        while (primes.Any())
        {
            var prime = primes.First();
            yield return prime;

            primes = primes.Where(n => n % prime != 0);
        }
    }
}